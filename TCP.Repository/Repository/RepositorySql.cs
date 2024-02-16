using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TCP.Repository.Interfaces;

namespace TCP.Repository.Repository
{
    public class RepositorySql : IRepositorySql , IDisposable
    {
        private readonly SqlConnection _connection;

        public RepositorySql(TCP.DataBaseContext.DataBaseContext context)
        {
            _connection = new SqlConnection(context.Database.GetConnectionString());
        }

        public IEnumerable<dynamic> ExecuteStoredProcedure(string storedProcedureName, DynamicParameters parameters)
        {
            _connection.Open();

            dynamic result = _connection.Query(storedProcedureName, parameters, null, true, null, CommandType.StoredProcedure);

            _connection.Close();

            return result;
        }

        public IEnumerable<dynamic> ExecuteQuery(string query)
        {
            _connection.Open();

            dynamic result = _connection.Query(query);

            _connection.Close();

            return result;
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
