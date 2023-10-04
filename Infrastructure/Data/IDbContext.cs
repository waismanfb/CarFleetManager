using System.Data;

namespace Infrastructure.Data
{
    public interface IDbContext
    {
        IDbConnection CreateConnection();
    }
}