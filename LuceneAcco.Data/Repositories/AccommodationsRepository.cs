using Dapper;
using LuceneAcco.Data.Abstractions;
using LuceneAcco.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneAcco.Data.Repositories
{
    public class AccommodationsRepository : IAccommodationsRepository
    {
        private readonly string connectionString = "connstring";
        public IEnumerable<Accommodation> GetAllAccommodations()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add("userName", "user");
            dynamicParameters.Add("statue", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            dynamicParameters.Add("message", dbType: System.Data.DbType.StringFixedLength, direction: System.Data.ParameterDirection.Output, size: 2000);
            using (var conn = new SqlConnection(connectionString))
            {
                var result = conn.Query<Accommodation>("storedProcedure_Get", dynamicParameters);
                var elapsedTime = stopwatch.Elapsed.TotalMilliseconds;
                return result;
            }
        }
    }
}
