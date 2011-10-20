using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace app.infrastructure.containers 
{
    public class ContainerFacade : IFetchDependencies
    {
        public Dependency an<Dependency>()
        {
            // create a fully populated new instance
            return null;
        }
    }

}
