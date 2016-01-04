using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Wicresoft.Solution.DAL
{
    internal class BulkOperationProvider
    {
        private DbContext _context;
        private string _connectionString;

        public BulkOperationProvider(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;

            //ConnectionStringSettings cs = ConfigurationManager.ConnectionStrings[context.GetType().Name];
            _connectionString = context.Database.Connection.ConnectionString;
        }

        public void Insert(DataTable dt, int batchSize)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();

                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        Insert(dt, transaction, SqlBulkCopyOptions.Default, batchSize);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        if (transaction.Connection != null)
                        {
                            transaction.Rollback();
                        }
                        throw;
                    }
                }
                //dbConnection.Close();
            }
        }

        private void Insert(DataTable dt, SqlTransaction transaction, SqlBulkCopyOptions options, int batchSize)
        {
            using (dt)
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(transaction.Connection, options, transaction))
                {
                    sqlBulkCopy.BatchSize = batchSize;
                    sqlBulkCopy.DestinationTableName = dt.TableName;
                    sqlBulkCopy.WriteToServer(dt);
                }
            }
        }

    }
}
