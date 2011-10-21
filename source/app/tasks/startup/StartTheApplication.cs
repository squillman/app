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
        static IRegisterComponentsIntoTheContainer container_registration;

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
            container_registration.add_factory<ViewTheMainDepartments>();
            container_registration.add_factory<ViewTheDepartmentsInADepartment>();
            container_registration.add_factory<ViewTheProductsInADepartment>();
        }

        static void configure_service_layer_components()
        {
            container_registration.add_factory<IProvideInformationAboutTheStore, StubStoreDirectory>();
        }

        static void configure_the_container()
        {
            container_facade =
                new ContainerFacade(new DependencyFactories(container_registration,
                                                            Stub.with<StubMissingDependencyFactory>().create));
            ContainerFacadeResolver resolver = () => container_facade;
            Container.facade_resolver = resolver;
        }

        static void configure_the_front_controller_components()
        {
            container_registration.add_factory<ICreateRequests, StubRequestFactory>();
            container_registration.add_factory<IProcessRequests, FrontController>();
            container_registration.add_factory<IEnumerable<IProcessOneSpecificTypeOfRequest>, StubSetOfCommands>();
            container_registration.add_factory<IDisplayInformation, WebFormReportEngine>();
            container_registration.add_factory<ICreateTemplateInstances, ASPXTemplateFactory>();
            container_registration.add_factory<IFindPathsToTemplates, StubAspxPathRegistry>();
            container_registration.add_factory<IFindCommandsThatCanProcessRequests, CommandRegistry>();
            container_registration.add_factory<IRepresentACommandThatIsNotYetSupported, StubMissingCommand>();
            container_registration.add_instance<GetTheCurrentlyExecutingContext>(() => HttpContext.Current);
            container_registration.add_instance<PageFactory>(BuildManager.CreateInstanceFromVirtualPath);
        }

    }
}