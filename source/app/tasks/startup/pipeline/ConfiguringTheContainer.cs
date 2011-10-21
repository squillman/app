using app.infrastructure.containers;
using app.infrastructure.containers.simple;
using app.infrastructure.containers.simple.stubs;
using app.web.infrastructure;

namespace app.tasks.startup.pipeline
{
    public class ConfiguringTheContainer : IPlayAPartInApplicationStartUp
    {
        IRegisterComponentsIntoTheContainer registration;

        public ConfiguringTheContainer(IRegisterComponentsIntoTheContainer registration)
        {
            this.registration = registration;
        }

        public void run()
        {
            var lazy_container = new LazyContainer();

            registration = new ContainerRegistrationFacility(new DependencyFactoriesFactory(lazy_container, new GreediestContructorPicker()));
            var container_facade = new ContainerFacade(new DependencyFactories(registration, Stub.with<StubMissingDependencyFactory>().create));
            lazy_container.container = container_facade;

            ContainerFacadeResolver resolver = () => container_facade;
            Container.facade_resolver = resolver;
        } 
    }
}