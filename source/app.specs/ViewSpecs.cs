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
    public class ViewSpecs
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
                the_view_model = fake.an<IDisplayCatalogItems>();
                department_repository = depends.on<IProvideInformationAboutTheStore>();
                view_directory = depends.on<IProvideViewModels>();
                report_engine = depends.on<IDisplayInformation>();

                request.setup(x => x.map<IProvideViewModels>()).Return();

            };

            Because b = () =>
                sut.process(request);

            It should_tell_the_report_engine_to_get_a_view_model_from_the_view_directory_and_display_its_details = () =>
                report_engine.received(x => x.display(the_view_model));

            static IDisplayInformation report_engine;
            static IContainRequestDetails request;
            static IProvideInformationAboutTheStore department_repository;
            static DepartmentItem parent_department;
            static ViewDepartmentsRequest department_details;
            static IDisplayCatalogItems the_view_model;
            static IProvideViewModels view_directory;
        }
    }
}