using System.Security;
using System.Threading;
using app.web.infrastructure;

namespace app.web.application.catalogbrowsing
{
    public class SecuredQuery<ReportModel> : IFetchA<ReportModel>
    {
        IFetchA<ReportModel> original;
        UserCriteria criteria;

        public SecuredQuery(IFetchA<ReportModel> original, UserCriteria criteria)
        {
            this.original = original;
            this.criteria = criteria;
        }

        public ReportModel query_using(IContainRequestDetails request)
        {
            if (criteria(Thread.CurrentPrincipal)) return original.query_using(request);

            throw new SecurityException("You are not authorized to run this query");
        }
    }
}