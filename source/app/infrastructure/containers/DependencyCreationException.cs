using System;

namespace app.infrastructure.containers
{
    public class DependencyCreationException:Exception
    {
        public Type type_that_could_not_be_created { get; set; }
    }
}