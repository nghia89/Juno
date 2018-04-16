using Juno.Data.Infrastructure;
using Juno.Model.Models;

namespace Juno.Data.Repositories
{
    public interface ISizeRepository : IRepository<Size>
    {
    }

    public class SizeRepository : RepositoryBase<Size>, ISizeRepository
    {
        public SizeRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}