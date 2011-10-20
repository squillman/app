using System.Collections.Generic;
using System.Linq;
using app.models;

namespace app.tasks.stubs
{
    public class StubStoreDirectory:IProvideInformationAboutTheStore
    {
        public IEnumerable<DepartmentItem> get_the_main_departments()
        {
            return Enumerable.Range(1, 100).Select(x => new DepartmentItem{name = x.ToString("Department 0")});
        }

        public IEnumerable<DepartmentItem> get_all_the_departments_in(ViewDepartmentsRequest parent_department)
        {
            return Enumerable.Range(1, 100).Select(x => new DepartmentItem{name = x.ToString("Sub Department 0")});
        }

        public IEnumerable<ProductItem> get_all_the_products_in(ViewProductsRequest request)
        {
            return Enumerable.Range(1, 100).Select(x => new ProductItem {name = x.ToString("Product 0")});
        }
    }
}