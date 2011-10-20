namespace app.web.infrastructure.aspnet
{
    public interface IFindPathsToTemplates
    {
        string get_path_to_template_that_can_display<ReportModel>();
    }
}