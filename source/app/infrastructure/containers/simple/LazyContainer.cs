using System;

namespace app.infrastructure.containers.simple
{
    public class LazyContainer:IFetchDependencies
    {
        public Dependency an<Dependency>()
        {
            return Container.fetch.an<Dependency>();
        }

        public object an(Type dependency)
        {
            return Container.fetch.an(dependency);
        }
    }
}