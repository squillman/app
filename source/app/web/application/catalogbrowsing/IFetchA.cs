using app.web.infrastructure;

namespace app.web.application.catalogbrowsing
{
    public interface IFetchA<out ReportModel>
    {
        ReportModel query_using(IContainRequestDetails request);
    }
}