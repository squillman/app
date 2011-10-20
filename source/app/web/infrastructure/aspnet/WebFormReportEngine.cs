namespace app.web.infrastructure.aspnet
{
    public class WebFormReportEngine : IDisplayInformation
    {
        private ICreateTemplateInstances viewFactory;

        public WebFormReportEngine(ICreateTemplateInstances viewFactory)
        {
            this.viewFactory = viewFactory;
        }

        public void display<ReportModel>(ReportModel item_to_display)
        {
            viewFactory.create_view_to_display(item_to_display);
        }
    }
}