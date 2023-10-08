using System.Data;

namespace Infrastructure.Data
{
    /// <summary>
    /// Interface for database context.
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// Creates a new database connection.
        /// </summary>
        /// <returns></returns>
        IDbConnection CreateConnection();
    }
}