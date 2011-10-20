using System.Collections.Generic;
using app.models;

namespace app.tasks
{
    public interface IProvideInformationAboutTheStore
    {
        IEnumerable<DepartmentItem> get_the_main_departments();
        IEnumerable<DepartmentItem> get_all_the_departments_in(ViewDepartmentsRequest parent_department);
        IEnumerable<ProductItem> get_all_the_products_in(ViewProductsRequest request);
    }
}