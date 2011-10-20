using Machine.Specifications;
using app.web.application.catalogbrowsing;
using app.web.infrastructure;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;

namespace app.specs
{
    public class ViewReportSpecs
    {
        public abstract class concern : Observes<IEncapsulateUserFunctionality,
                                            ViewReport<SomeModel>>
        {
        }

        public class when_run : concern
        {
            Establish c = () =>
            {
                report_engine = depends.on<IDisplayInformation>();
                query = depends.on<IFetchA<SomeModel>>();
                request = fake.an<IContainRequestDetails>();
                the_model = new SomeModel();

                query.setup(x => x.query_using(request)).Return(the_model);
            };

            Because b = () =>
                sut.process(request);

            It should_tell_the_report_engine_to_display_the_departments_within_a_department = () =>
                report_engine.received(x => x.display(the_model));

            static IDisplayInformation report_engine;
            static IContainRequestDetails request;
            static SomeModel the_model;
            static IFetchA<SomeModel> query;
        }
    }

    public class SomeModel
    {
    }
}