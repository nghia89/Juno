using Juno.Data.Infrastructure;
using Juno.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juno.Data.Repositories
{
    public interface INewRepository : IRepository<New>
    {

    }
    public class NewRepository : RepositoryBase<New>, INewRepository
    {
        public NewRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
