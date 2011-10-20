using System.Web;
using Machine.Specifications;
using app.specs.utility;
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
                view = fake.an<IHttpHandler>();
                the_currently_executing_request = ObjectFactory.web.create_http_context();

                view_factory = depends.on<ICreateTemplateInstances>();
                depends.on(the_currently_executing_request);

                view_factory.setup(x => x.create_view_to_display(report_model)).Return(view);
            };

            Because b = () =>
                sut.display(report_model);


            It should_tell_the_view_to_process_using_the_current_context = () =>
                view.received(x => x.ProcessRequest(the_currently_executing_request));
                

            static TheModel report_model;
            static ICreateTemplateInstances view_factory;
            static IHttpHandler view;
            static HttpContext the_currently_executing_request;
        }

        public class TheModel
        {
        }
    }
}