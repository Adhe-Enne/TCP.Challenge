using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TCP.Repository.Interfaces;
using static Dapper.SqlMapper;

namespace TCP.Repository.Repository
{
    public class RepositorySql<T> : IRepositorySql<T> where T : class
    {
        private readonly SqlConnection _connection;

        public RepositorySql(TCP.DataBaseContext.DataBaseContext context)
        {
            _connection = new SqlConnection(context.Database.GetConnectionString());
        }

        public IEnumerable<T> ExecuteStoredProcedure(string storedProcedureName, DynamicParameters parameters)
        {
            _connection.Open();
            return _connection.Query<T>(storedProcedureName, parameters, null , true , null , CommandType.StoredProcedure);
        }

        public IEnumerable<T> ExecuteQuery(string query)
        {
            return _connection.Query<T>(query);
        }
    }
}
