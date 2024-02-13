using Dapper;

namespace TCP.Repository.Interfaces
{
    /// <summary>
    /// Repository Custom, ejecuto Store Procedures y Query de manera dinamica para mantener generalidad.
    /// Solo para este Repository utilizo tipo de dato Dynamic como retorno para no limitar la escalabilidad 
    /// ni hacer implementaciones excesivas para retornar entidades ajenas al DbContext
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositorySql
    {
        IEnumerable<dynamic> ExecuteStoredProcedure(string storedProcedureName, DynamicParameters parameters);
        IEnumerable<dynamic> ExecuteQuery(string query);
    }
}
