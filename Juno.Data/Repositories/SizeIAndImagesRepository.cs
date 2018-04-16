using Juno.Data.Infrastructure;
using Juno.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juno.Data.Repositories
{
    public interface ISizeAndImagesRepository : IRepository<FavoriteColor>
    {

    }
    public class SizeAndImagesRepository: RepositoryBase<FavoriteColor>, ISizeAndImagesRepository
    {
        public SizeAndImagesRepository(IDbFactory dbFactory):base(dbFactory)
        {

        }
    }
}
