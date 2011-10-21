using System;
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
        static IDictionary<Type, ICreateASingleDependency> all_the_factories =
            new Dictionary<Type, ICreateASingleDependency>();

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
            add_factory<ViewTheMainDepartments>();
            add_factory<ViewTheDepartmentsInADepartment>();
            add_factory<ViewTheProductsInADepartment>();
        }

        static void configure_service_layer_components()
        {
            add_factory<IProvideInformationAboutTheStore,StubStoreDirectory>();
        }

        static void configure_the_container()
        {
            container_facade = new ContainerFacade(new DependencyFactories(all_the_factories,
                                                                           Stub.with<StubMissingDependencyFactory>().create));
            ContainerFacadeResolver resolver = () => container_facade;
            Container.facade_resolver = resolver;
        }

        static void configure_the_front_controller_components()
        {
            add_factory<ICreateRequests, StubRequestFactory>();
            add_factory<IProcessRequests, FrontController>();
            add_factory<IEnumerable<IProcessOneSpecificTypeOfRequest>,StubSetOfCommands>();
            add_factory<IDisplayInformation, WebFormReportEngine>();
            add_factory<ICreateTemplateInstances,ASPXTemplateFactory>();
            add_factory<IFindPathsToTemplates,StubAspxPathRegistry>();
            add_factory<IFindCommandsThatCanProcessRequests,CommandRegistry>();
            add_instance<GetTheCurrentlyExecutingContext>(() => HttpContext.Current);
            add_instance<PageFactory>(BuildManager.CreateInstanceFromVirtualPath);
        }

        static void add_instance<Contract>(Contract instance)
        {
            all_the_factories.Add(typeof(Contract),new SimpleDependencyFactory(() => instance));
        }

        static void add_factory<Contract,Implementation>()
        {
            all_the_factories.Add(typeof(Contract), new AutomaticallyWiringDependencyFactory(container_facade,
                                                                                             typeof(Implementation),
                                                                                             new GreediestContructorPicker
                                                                                                 ()));
        }
        static void add_factory<Contract>()
        {
            add_factory<Contract,Contract>();
        }

    }
}
