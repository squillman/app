using System.Collections.Generic;
using System.Web;
using System.Web.Compilation;
using app.infrastructure.containers;
using app.infrastructure.containers.simple;
using app.infrastructure.containers.simple.stubs;
using app.tasks.stubs;
using app.web.application.catalogbrowsing;
using app.web.infrastructure;
using app.web.infrastructure.aspnet;
using app.web.infrastructure.stubs;

namespace app.tasks.startup
{
    public class StartTheApplication
    {
        static IRegisterComponentsIntoTheContainer registration;

        static IFetchDependencies container_facade;

        public static void run()
        {
            configure_the_container();
            configure_the_front_controller_components();
            configure_service_layer_components();
            configure_the_user_commands();
        }

        static void configure_the_user_commands()
        {
            registration.register<ViewTheMainDepartments>();
            registration.register<ViewTheDepartmentsInADepartment>();
            registration.register<ViewTheProductsInADepartment>();
        }

        static void configure_service_layer_components()
        {
            registration.register<IProvideInformationAboutTheStore, StubStoreDirectory>();
        }

        static void configure_the_container()
        {
            container_facade =
                new ContainerFacade(new DependencyFactories(registration,
                                                            Stub.with<StubMissingDependencyFactory>().create));
            ContainerFacadeResolver resolver = () => container_facade;
            Container.facade_resolver = resolver;
        }

        static void configure_the_front_controller_components()
        {
            registration.register<ICreateRequests, StubRequestFactory>();
            registration.register<IProcessRequests, FrontController>();
            registration.register<IEnumerable<IProcessOneSpecificTypeOfRequest>, StubSetOfCommands>();
            registration.register<IDisplayInformation, WebFormReportEngine>();
            registration.register<ICreateTemplateInstances, ASPXTemplateFactory>();
            registration.register<IFindPathsToTemplates, StubAspxPathRegistry>();
            registration.register<IFindCommandsThatCanProcessRequests, CommandRegistry>();
            registration.register<IRepresentACommandThatIsNotYetSupported, StubMissingCommand>();
            registration.register_instance<GetTheCurrentlyExecutingContext>(() => HttpContext.Current);
            registration.register_instance<PageFactory>(BuildManager.CreateInstanceFromVirtualPath);
        }

    }
}