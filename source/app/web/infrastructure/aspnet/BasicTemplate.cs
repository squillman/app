using System.Web.UI;

namespace app.web.infrastructure.aspnet
{
    public class BasicTemplate<ReportModel> : Page,IRenderA<ReportModel>
    {
        public ReportModel report_model { get; set; }
    }
}