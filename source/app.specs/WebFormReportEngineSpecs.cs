using Machine.Specifications;
using app.web.infrastructure;
using app.web.infrastructure.aspnet;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace app.specs
{
    [Subject(typeof(WebFormReportEngine))]
    public class WebFormReportEngineSpecs
    {
        public abstract class concern : Observes<IDisplayInformation,
                                            WebFormReportEngine>
        {
        }

        public class when_displaying_a_report_model : concern
        {
            Establish c = () =>
            {
                report_model = new TheModel();
                view_factory = depends.on<ICreateTemplateInstances>();
            };

            Because b = () =>
                sut.display(report_model);

            It should_create_the_view_that_can_display_the_report = () =>
                view_factory.received(x => x.create_view_to_display(report_model));


            static TheModel report_model;
            static ICreateTemplateInstances view_factory;
        }

        public class TheModel
        {
        }
    }
}