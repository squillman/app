using System;

namespace app.infrastructure.containers
{
    public class ContainerFacade : IFetchDependencies
    {
        IFindFactoriesForDependencies dependency_factories;

        public ContainerFacade(IFindFactoriesForDependencies dependency_factories)
        {
            this.dependency_factories = dependency_factories;
        }

        public Dependency an<Dependency>()
        {
            return (Dependency) an(typeof(Dependency));
        }

        public object an(Type dependency)
        {
            try
            {
                return dependency_factories.get_the_factory_that_can_create(dependency).create();
            }
            catch (Exception ex)
            {
                throw new DependencyCreationException(ex,dependency);
            }
        }
    }
}