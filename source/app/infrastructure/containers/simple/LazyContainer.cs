using System;

namespace app.infrastructure.containers.simple
{
    public class LazyContainer:IFetchDependencies
    {
        public IFetchDependencies container { get; set; }

        public Dependency an<Dependency>()
        {
            return container.an<Dependency>();
        }

        public object an(Type dependency)
        {
            return container.an(dependency);
        }
    }
}