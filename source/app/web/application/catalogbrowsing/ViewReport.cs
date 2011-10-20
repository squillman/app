using app.web.infrastructure;
using app.web.infrastructure.stubs;

namespace app.web.application.catalogbrowsing
{
    public class ViewReport<ReportModel> : IEncapsulateUserFunctionality
    {
        IFetchA<ReportModel> query;
        IDisplayInformation report_engine;

        public ViewReport(IFetchA<ReportModel> query, IDisplayInformation report_engine)
        {
            this.query = query;
            this.report_engine = report_engine;
        }

        public ViewReport(IFetchA<ReportModel> query):this(query,Stub.with<StubReportEngine>())
        {
        }

        public void process(IContainRequestDetails request)
        {
            report_engine.display(query.query_using(request));
        }
    }
}