namespace app.web.infrastructure.aspnet
{
    public class WebFormReportEngine : IDisplayInformation
    {
        private ICreateTemplateInstances view_factory;

        public WebFormReportEngine(ICreateTemplateInstances viewFactory)
        {
            this.view_factory = viewFactory;
        }

        public void display<ReportModel>(ReportModel item_to_display)
        {
            view_factory.create_view_to_display(item_to_display);
        }
    }
}