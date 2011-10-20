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
    [Subject(typeof(ViewTheProductsInADepartment))]
    public class ViewTheProductsInADepartmentSpecs
    {
        public abstract class concern : Observes<IEncapsulateUserFunctionality,
                                            ViewTheProductsInADepartment>
        {
        }

        public class when_run : concern
        {
            Establish c = () =>
            {
                request = fake.an<IContainRequestDetails>();
                product_repository = depends.on<IProvideInformationAboutTheStore>();
                the_products_in_the_department = new List<ProductItem> {new ProductItem()};
                report_engine = depends.on<IDisplayInformation>();
                department_details = new ViewProductsRequest();

                request.setup(x => x.map<ViewProductsRequest>()).Return(department_details);

                product_repository.setup(x => x.get_all_the_products_in(department_details)).Return(
                    the_products_in_the_department);
            };

            Because b = () =>
                sut.process(request);

            It should_tell_the_report_engine_to_display_the_products_within_a_department = () =>
                report_engine.received(x => x.display(the_products_in_the_department));
        }

        static IDisplayInformation report_engine;
        static IContainRequestDetails request;
        static IProvideInformationAboutTheStore product_repository;
        static ViewProductsRequest department_details;
        static IEnumerable<ProductItem> the_products_in_the_department;
    }
}