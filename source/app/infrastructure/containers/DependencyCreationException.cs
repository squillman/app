using System;

namespace app.infrastructure.containers
{
    public class DependencyCreationException : Exception
    {
        public Type type_that_could_not_be_created { private set; get; }

        public DependencyCreationException(Exception inner_exception, Type dependency_type)
            : base(string.Empty, inner_exception)
        {
            type_that_could_not_be_created = dependency_type;
        }
    }
}