using Juno.Data.Infrastructure;
using Juno.Model.Models;

namespace Juno.Data.Repositories
{
    public interface IOrderDetailRepository :  IRepository<OrderDetail>
    {
    }

    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}