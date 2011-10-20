using app.tasks;
using app.tasks.stubs;
using app.web.infrastructure;
using app.web.infrastructure.aspnet;

namespace app.web.application.catalogbrowsing
{
    public class ViewTheMainDepartments : IEncapsulateUserFunctionality
    {
        IProvideInformationAboutTheStore store_directory;
        IDisplayInformation report_engine;

        public ViewTheMainDepartments():this(Stub.with<StubStoreDirectory>(),
            new WebFormReportEngine())
        {
        }

        public ViewTheMainDepartments(IProvideInformationAboutTheStore store_directory, IDisplayInformation report_engine)
        {
            this.store_directory = store_directory;
            this.report_engine = report_engine;
        }

        public void process(IContainRequestDetails request)
        {
            report_engine.display(store_directory.get_the_main_departments());
        }
    }
}