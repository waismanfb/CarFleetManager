using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Data
{
    /// <summary>
    /// Represents a database context.
    /// </summary>
    public class DbContext : IDbContext
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContext"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public DbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CarFleetManager");
        }

        /// <summary>
        /// Creates a new database connection.
        /// </summary>
        /// <returns>The database connection.</returns>
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
