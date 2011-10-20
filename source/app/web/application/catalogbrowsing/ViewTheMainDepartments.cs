using app.tasks;
using app.web.infrastructure;

namespace app.web.application.catalogbrowsing
{
    public class ViewTheMainDepartments : IEncapsulateUserFunctionality
    {
        IProvideInformationAboutTheStore store_directory;
        IDisplayInformation report_engine;

        public ViewTheMainDepartments(IProvideInformationAboutTheStore store_directory,
                                      IDisplayInformation report_engine)
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