using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.Solution.DAL
{
    internal static class DbContextBulkOperationExtensions
    {
        public const int DefaultBatchSize = 1000;

        public static void BulkInsert(this DbContext context, DataTable dt, int batchSize = DefaultBatchSize)
        {
            var provider = new BulkOperationProvider(context);
            provider.Insert(dt, batchSize);
        }
    }
}
