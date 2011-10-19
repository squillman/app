using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace app.web.infrastructure
{
    public class CommandRegistry : IFindCommandsThatCanProcessRequests
    {
        public IProcessOneSpecificTypeOfRequest get_the_command_that_can_process(IContainRequestDetails request)
        {
            return new SpecificRequestCommand();
        }
    }
}
