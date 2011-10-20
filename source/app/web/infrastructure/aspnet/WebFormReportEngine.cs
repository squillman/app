using System.Web;

namespace app.web.infrastructure.aspnet
{
    public class WebFormReportEngine : IDisplayInformation
    {
        ICreateTemplateInstances view_factory;
        GetTheCurrentlyExecutingContext context_resolver;

        public WebFormReportEngine(ICreateTemplateInstances viewFactory,
                                   GetTheCurrentlyExecutingContext context_resolver)
        {
            this.view_factory = viewFactory;
            this.context_resolver = context_resolver;
        }

        public WebFormReportEngine():this(new ASPXTemplateFactory(),() => HttpContext.Current )
        {
        }

        public void display<ReportModel>(ReportModel item_to_display)
        {
            view_factory.create_view_to_display(item_to_display).ProcessRequest(context_resolver());
        }
    }
}