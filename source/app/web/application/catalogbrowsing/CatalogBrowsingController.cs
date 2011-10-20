using System.Collections.Generic;
using app.models;
using app.tasks;

namespace app.web.application.catalogbrowsing
{
    public class CatalogBrowsingController
    {
        IProvideInformationAboutTheStore store;

        public CatalogBrowsingController(IProvideInformationAboutTheStore store)
        {
            this.store = store;
        }

        public IEnumerable<DepartmentItem> get_the_main_departments()
        {
            return store.get_the_main_departments();
        }

        public IEnumerable<DepartmentItem> get_all_the_departments_in(ViewDepartmentsRequest parent_department)
        {
            return store.get_all_the_departments_in(parent_department);
        }

        public IEnumerable<ProductItem> get_all_the_products_in(ViewProductsRequest request)
        {
            return store.get_all_the_products_in(request);
        }
    }
}