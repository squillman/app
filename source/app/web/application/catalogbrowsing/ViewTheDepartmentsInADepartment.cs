using app.models;
using app.tasks;
using app.tasks.stubs;
using app.web.infrastructure;
using app.web.infrastructure.stubs;

namespace app.web.application.catalogbrowsing
{
    public class ViewTheDepartmentsInADepartment:IEncapsulateUserFunctionality
    {
        IProvideInformationAboutTheStore department_repository;
        IDisplayInformation report_engine;

        public ViewTheDepartmentsInADepartment():this(Stub.with<StubStoreDirectory>(),
            Stub.with<StubReportEngine>())
        {
        }

        public ViewTheDepartmentsInADepartment(IProvideInformationAboutTheStore department_repository, IDisplayInformation report_engine)
        {
            this.department_repository = department_repository;
            this.report_engine = report_engine;
        }

        public void process(IContainRequestDetails request)
        {
            report_engine.display(department_repository.get_all_the_departments_in(request.map<ViewDepartmentsRequest>()));
        }
    }
}