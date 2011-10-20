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
            var factory = dependency_factories.get_the_factory_that_can_create(typeof(Dependency));
            return (Dependency) factory.create();
        }
    }
}