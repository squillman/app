using System;

namespace app.infrastructure.containers.simple
{
    public class SingleDependencyFactory : ICreateASingleDependency
    {
        private Type dependency_type;
        private SimpleDependencyFactory simple_dependency_factory;

        public SingleDependencyFactory(Type dependencyType, SimpleDependencyFactory simpleDependencyFactory)
        {
            dependency_type = dependencyType;
            simple_dependency_factory = simpleDependencyFactory;
        }

        public object create()
        {
            return simple_dependency_factory.create(x => );
        }
    }
}