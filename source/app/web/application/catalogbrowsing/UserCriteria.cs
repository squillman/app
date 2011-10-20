using System.Security.Principal;

namespace app.web.application.catalogbrowsing
{
    public delegate bool UserCriteria(IPrincipal principal);
}