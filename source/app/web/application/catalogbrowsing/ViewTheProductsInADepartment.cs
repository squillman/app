using app.models;
using app.tasks;
using app.tasks.stubs;
using app.web.infrastructure;
using app.web.infrastructure.stubs;

namespace app.web.application.catalogbrowsing
{
    public class ViewTheProductsInADepartment:IEncapsulateUserFunctionality
    {
        IFindProducts product_repository;
        IDisplayInformation report_engine;

        public ViewTheProductsInADepartment():this(Stub.with<StubProductRepository>(),Stub.with<StubReportEngine>())
        {
            
        }

        public ViewTheProductsInADepartment(IFindProducts productRepository, IDisplayInformation reportEngine)
        {
            product_repository = productRepository;
            report_engine = reportEngine;
        }

        public void process(IContainRequestDetails request)
        {
            report_engine.display(product_repository.get_all_the_products_in(request.map<DepartmentItem>()));
        }
    }
}