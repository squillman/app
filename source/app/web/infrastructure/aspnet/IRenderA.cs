using System.Web;

namespace app.web.infrastructure.aspnet
{
    public interface IRenderA<ReportModel> : IHttpHandler
    {
        ReportModel report_model { get; set; }
    }
}