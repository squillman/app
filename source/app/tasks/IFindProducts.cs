using System.Collections.Generic;
using app.models;

namespace app.tasks
{
    public interface IFindProducts
    {
        IEnumerable<ProductItem> get_all_the_products_in(DepartmentItem parent_department);
    }
}