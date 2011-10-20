using System.Web;

namespace app.web.infrastructure.aspnet
{
    public interface ICreateTemplateInstances
    {
        IHttpHandler create_view_to_display<ReportModel>(ReportModel report_model);
    }
}