using System.Collections.Generic;
using System.Linq;
using app.models;

namespace app.tasks.stubs
{
    public class StubProductRepository : IFindProducts
    {
        public IEnumerable<ProductItem> get_all_the_products_in(DepartmentItem parent_department)
        {
            return Enumerable.Range(1, 25).Select(x => new ProductItem {name = x.ToString("Product 0")});
        }
    }
}