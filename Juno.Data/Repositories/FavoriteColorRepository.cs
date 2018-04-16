using Juno.Data.Infrastructure;
using Juno.Model.Models;

namespace Juno.Data.Repositories
{
    public interface IFavoriteColorRepositories : IRepository<FavoriteColor>
    {

    }
    public class FavoriteColorRepositories : RepositoryBase<FavoriteColor>, IFavoriteColorRepositories
    {
        public FavoriteColorRepositories(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}