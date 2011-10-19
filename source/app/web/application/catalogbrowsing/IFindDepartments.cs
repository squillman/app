using System.Collections.Generic;

namespace app.web.application.catalogbrowsing
{
    public interface IFindDepartments
    {
        IEnumerable<string> get_the_main_departments();
    }
}