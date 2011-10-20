using System;
using System.Runtime.Serialization;

namespace app.infrastructure.containers
{
    public class DependencyCreationException:Exception
    {
        public DependencyCreationException(string message, Exception inner_exception, Type dependencyType) : base(message,inner_exception)
        {
            type_that_could_not_be_created = dependencyType;
        }

        public Type type_that_could_not_be_created { get; set; }
    }
}