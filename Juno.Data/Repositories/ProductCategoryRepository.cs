using Juno.Data.Infrastructure;
using Juno.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Juno.Data.Repositories
{
    //lớp kế thừa các phương thức định sẳn nếu cần viết thêm
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        IEnumerable<ProductCategory> GetByAlias(string alias);
    }

    public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<ProductCategory> GetByAlias(string alias)
        {
            return this.DbContext.ProductCategories.Where(x => x.Alias == alias);
        }
    }
}