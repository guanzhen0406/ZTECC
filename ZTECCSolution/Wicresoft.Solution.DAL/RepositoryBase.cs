using System.Data.Entity.Validation;
using System.Text;
using Wicresoft.Solution.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Wicresoft.Solution.Utils;

namespace Wicresoft.Solution.DAL
{
    public class RepositoryBase<TModel, TContext>
        where TModel : class
        where TContext : DbContext
    {

        private TContext _TContext;

        public TContext CreateConnection()
        {
            if (typeof(TContext) == typeof(ZTECCEntities))
            {
                _TContext = new ZTECCEntities() as TContext;
            }
            ((System.Data.Entity.Infrastructure.IObjectContextAdapter)_TContext).ObjectContext.CommandTimeout = 0;
            return _TContext;
        }

        public TModel Get(int id)
        {
            using (var _context = CreateConnection())
            {
                return _context.Set<TModel>().Find(id);
            }
        }

        public TModel Get(Guid guid)
        {
            using (var _context = CreateConnection())
            {
                return _context.Set<TModel>().Find(guid);
            }
        }

        public TModel GetFirst(Expression<Func<TModel, bool>> where)
        {
            using (var _context = CreateConnection())
            {
                return _context.Set<TModel>().FirstOrDefault(where);
            }
        }

        public List<TModel> Get(Expression<Func<TModel, bool>> where)
        {
            using (var _context = CreateConnection())
            {
                return _context.Set<TModel>().Where(where).ToList();
            }

        }

        public virtual List<TModel> All
        {
            get
            {
                using (var _context = CreateConnection())
                {
                    return _context.Set<TModel>().ToList();
                }
            }
        }

        public void Add(TModel entity)
        {
            DbOperate((dbSet, ctx) =>
            {
                ctx.Entry(entity).State = EntityState.Added;
                ctx.Set<TModel>().Add(entity);
            });
        }

        /// <summary>
        /// 批量添加,建议使用此
        /// </summary>
        /// <param name="entities"></param>
        public void AddManyByBulk(List<TModel> entities)
        {
            using (var _context = CreateConnection())
            {
                var dt = entities.ToDataTable<TModel>();
                _context.BulkInsert(dt);
                _context.SaveChanges();
            }
        }

        public void AddManyTwo(List<TModel> entities)
        {
            using (var _context = CreateConnection())
            {
                foreach (var entity in entities)
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.Set<TModel>().Add(entity);

                }
                _context.SaveChanges();
            }
        }

        public TModel AddModel(TModel entity)
        {
            using (var _context = CreateConnection())
            {
                _context.Entry(entity).State = EntityState.Added;
                var temp = _context.Set<TModel>().Add(entity);
                _context.SaveChanges();
                return temp;
            }
        }

        /// <summary>
        /// 效率问题,建议使用 AddManyByBulk
        /// </summary>
        /// <param name="entities"></param>
        public void AddMany(List<TModel> entities)
        {
            foreach (var entity in entities)
            {
                Add(entity);
            }
        }

        public void Update(TModel entity)
        {
            DbOperate((dbSet, ctx) =>
            {
                ctx.Entry(entity).State = EntityState.Modified;
            });
        }

        public void UpdateMany(List<TModel> entities)
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }

        public void UpdateManyTwo(List<TModel> entities)
        {
            using (var _context = CreateConnection())
            {
                foreach (var entity in entities)
                {
                    _context.Entry(entity).State = EntityState.Modified;
                    //_context.Set<TModel>().Add(entity);

                }
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            DbOperate((dbSet, ctx) =>
            {
                var entity = Get(id);
                ctx.Entry(entity).State = EntityState.Deleted;
                ctx.Set<TModel>().Remove(entity);
            });
        }

        public void Delete(Guid guid)
        {
            DbOperate((dbSet, ctx) =>
            {
                var entity = Get(guid);
                ctx.Entry(entity).State = EntityState.Deleted;
                ctx.Set<TModel>().Remove(entity);
            });
        }

        public List<T> ExecSqlQuery<T>(string sql, params SqlParameter[] parameters)
        {
            return ExecSqlQuery<T, int>(sql, null, false, 0, 0, parameters);
        }

        public List<T> ExecSqlQuery<T, TKey>(string sql, Func<T, TKey> orderby, bool asc, int pageIndex, int PageSize, params SqlParameter[] parameters)
        {
            using (var _context = CreateConnection())
            {
                ((System.Data.Entity.Infrastructure.IObjectContextAdapter)_context).ObjectContext.CommandTimeout = 0;
                var query = _context.Database.SqlQuery<T>(sql, parameters);
                if (orderby != null)
                {
                    if (asc)
                        query = query.OrderBy(orderby);
                    else
                        query = query.OrderByDescending(orderby);
                }
                if (pageIndex > 0 && PageSize > 0)
                {
                    query = query.Skip((pageIndex - 1) * PageSize);
                    return query.Take(PageSize).ToList();
                }
                return query.ToList();
            }
        }

        public List<TModel> GetByPage<TKey>(int startIndex, int pageSize, Expression<Func<TModel, bool>> filter, Expression<Func<TModel, TKey>> order, bool desc, out int recordTotal)
        {
            using (var _context = CreateConnection())
            {
                IQueryable<TModel> query = _context.Set<TModel>().AsQueryable();
                if (filter != null)
                {
                    query = query.Where(filter);
                }
                if (order != null)
                {
                    if (desc)
                        query = query.OrderByDescending(order);
                    else
                        query = query.OrderBy(order);
                }
                if (startIndex < 1)
                {
                    startIndex = 1;
                }
                recordTotal = query.Count();
                return query.Skip((startIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int ExecSqlCommand(string sql, params SqlParameter[] parameters)
        {
            using (var _context = CreateConnection())
            {
                ((System.Data.Entity.Infrastructure.IObjectContextAdapter)_context).ObjectContext.CommandTimeout = 0;
                return _context.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        private void DbOperate(Action<DbSet<TModel>, DbContext> action)
        {
            using (var ctx = CreateConnection())
            {
                try
                {
                    var dbSet = ctx.Set<TModel>();
                    action(dbSet, ctx);
                    ctx.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    var sb = new StringBuilder();
                    var methodName = action.Method.Name;
                    var realMethodName = methodName.Substring(1, methodName.IndexOf(">") - 1);
                    sb.AppendFormat("{0} Exception", realMethodName);
                    foreach (var eve in dbEx.EntityValidationErrors)
                    {
                        sb.AppendLine("ValidationErrors count is " + eve.ValidationErrors.Count);
                        foreach (var e in eve.ValidationErrors)
                        {
                            sb.AppendLine();
                            sb.AppendFormat("ErrorMessage:[{0}],PropertyName:[{1}]", e.ErrorMessage, e.PropertyName);
                        }
                    }
                    Log.Error(sb.ToString());
                    throw dbEx.InnerException ?? dbEx;
                }
            }
        }

        public DataTable SqlQueryForDataTable(Database db, string sql, SqlParameter[] parameters)
        {
            SqlConnection conn = new System.Data.SqlClient.SqlConnection();
            conn.ConnectionString = db.Connection.ConnectionString;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.CommandTimeout = 0;
            if (parameters.Length > 0)
            {
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
