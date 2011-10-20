using System.Web;

namespace app.web.infrastructure.aspnet
{
    public class WebFormReportEngine : IDisplayInformation
    {
        private ICreateTemplateInstances view_factory;
        private HttpContext current_context;

        public WebFormReportEngine(ICreateTemplateInstances viewFactory, HttpContext currentContext)
        {
            this.view_factory = viewFactory;
            current_context = currentContext;
        }

        public void display<ReportModel>(ReportModel item_to_display)
        {
            view_factory.create_view_to_display(item_to_display).ProcessRequest(current_context);
        }
    }
}