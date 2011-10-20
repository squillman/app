using System.Collections.Generic;
using app.models;
using app.web.infrastructure.aspnet;

namespace app.web.infrastructure.stubs
{
    public class StubAspxPathRegistry : IFindPathsToTemplates
    {
        public string get_path_to_template_that_can_display<ReportModel>()
        {
            if (typeof(IEnumerable<DepartmentItem>).Equals(typeof(ReportModel))) return create_path_to("DepartmentBrowser");

            return create_path_to("ProductBrowser");
        }

        string create_path_to(string page)
        {
            return string.Format("~/views/{0}.aspx",page);
        }
    }
}