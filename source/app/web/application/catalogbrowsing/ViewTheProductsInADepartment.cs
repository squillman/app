using app.models;
using app.tasks;
using app.tasks.stubs;
using app.web.infrastructure;
using app.web.infrastructure.aspnet;

namespace app.web.application.catalogbrowsing
{
    public class ViewTheProductsInADepartment:IEncapsulateUserFunctionality
    {
        IProvideInformationAboutTheStore store_directory;
        IDisplayInformation report_engine;

        public ViewTheProductsInADepartment():this(Stub.with<StubStoreDirectory>(),
        new WebFormReportEngine())
        {
        }

        public ViewTheProductsInADepartment(IProvideInformationAboutTheStore store_directory, IDisplayInformation report_engine)
        {
            this.store_directory = store_directory;
            this.report_engine = report_engine;
        }

        public void process(IContainRequestDetails request)
        {
            report_engine.display(store_directory.get_all_the_products_in(request.map<ViewProductsRequest>()));
        }
    }
}