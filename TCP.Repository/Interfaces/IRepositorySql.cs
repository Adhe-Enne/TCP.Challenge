using Dapper;

namespace TCP.Repository.Interfaces
{
    public interface IRepositorySql<T> where T : class
    {
        IEnumerable<T> ExecuteStoredProcedure(string storedProcedureName, DynamicParameters parameters);
        IEnumerable<T> ExecuteQuery(string query);
    }
}
