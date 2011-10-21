using System;

namespace app.infrastructure.containers.simple.stubs
{
    public class StubMissingDependencyFactory : ICreateASingleDependency
    {
        Type type;

        public StubMissingDependencyFactory(Type type)
        {
            this.type = type;
        }

        public StubMissingDependencyFactory()
        {
        }

        public ICreateASingleDependency create(Type type_that_has_no_factory)
        {
            return new StubMissingDependencyFactory(type_that_has_no_factory);

        }

        public object create()
        {
            throw new NotImplementedException(string.Format("There is no factory for the type {0}",type.Name));
        }
    }
}