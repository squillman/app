using app.web.infrastructure;

namespace app.web.application.catalogbrowsing
{
    public class ViewTheMainDepartments : IEncapsulateUserFunctionality
    {
        IFindDepartments department_repository;

        public ViewTheMainDepartments(IFindDepartments departmentRepository)
        {
            department_repository = departmentRepository;
        }

        public void process(IContainRequestDetails request)
        {
            throw new System.NotImplementedException();
        }
    }
}