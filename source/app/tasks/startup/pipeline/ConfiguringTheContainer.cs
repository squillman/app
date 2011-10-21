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
            registration = Factories.registration_factory();

            var container_facade = new ContainerFacade(new DependencyFactories(registration, Stub.with<StubMissingDependencyFactory>().create));
            ContainerFacadeResolver resolver = () => container_facade;
            Container.facade_resolver = resolver;
        } 
    }
}