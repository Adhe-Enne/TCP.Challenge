using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TCP.Repository.Interfaces;

namespace TCP.Repository.Repository
{
    public class RepositorySql : IRepositorySql
    {
        private readonly SqlConnection _connection;

        public RepositorySql(TCP.DataBaseContext.DataBaseContext context)
        {
            _connection = new SqlConnection(context.Database.GetConnectionString());
        }

        public IEnumerable<dynamic> ExecuteStoredProcedure(string storedProcedureName, DynamicParameters parameters)
        {
            _connection.Open();
            return _connection.Query(storedProcedureName, parameters, null , true , null , CommandType.StoredProcedure);
        }

        public IEnumerable<dynamic> ExecuteQuery(string query)
        {
            return _connection.Query(query);
        }
    }
}
