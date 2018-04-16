using Juno.Data.Infrastructure;
using Juno.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juno.Data.Repositories
{
    public interface IprovincesRepository:IRepository<province>
    {

    }
  public  class provincesRepository:RepositoryBase<province>,IprovincesRepository
    {
        public provincesRepository(IDbFactory dbFactory):base(dbFactory)
        {

        }
    }
}
