using System.Collections.Generic;
using Machine.Specifications;
using app.models;
using app.tasks;
using app.web.application.catalogbrowsing;
using app.web.infrastructure;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;

namespace app.specs
{
    [Subject(typeof(ViewTheDepartmentsInADepartment))]
    public class ViewTheDepartmentsInADepartmentSpecs
    {
        public abstract class concern : Observes<IEncapsulateUserFunctionality,
                                            ViewTheDepartmentsInADepartment>
        {
        }

        public class when_run : concern
        {
            Establish c = () =>
            {
                request = fake.an<IContainRequestDetails>();
                department_repository = depends.on<IProvideInformationAboutTheStore>();
                the_departments_in_the_department = new List<DepartmentItem> {new DepartmentItem()};
                report_engine = depends.on<IDisplayInformation>();
                department_details = new ViewDepartmentsRequest();

                request.setup(x => x.map<ViewDepartmentsRequest>()).Return(department_details);

                department_repository.setup(x => x.get_all_the_departments_in(department_details)).Return(
                    the_departments_in_the_department);
            };

            Because b = () =>
                sut.process(request);

            It should_tell_the_report_engine_to_display_the_departments_within_a_department = () =>
                report_engine.received(x => x.display(the_departments_in_the_department));

            static IDisplayInformation report_engine;
            static IContainRequestDetails request;
            static IProvideInformationAboutTheStore department_repository;
            static DepartmentItem parent_department;
            static IEnumerable<DepartmentItem> the_departments_in_the_department;
            static ViewDepartmentsRequest department_details;
        }
    }
}