namespace app.web.infrastructure
{
    public interface ICreateTemplateInstances
    {
        void create_view_to_display<ReportModel>(ReportModel report_model);
    }
}