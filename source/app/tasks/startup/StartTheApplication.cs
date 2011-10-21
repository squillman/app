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

        public static void run()
        {
            configure_the_container();
            configure_the_front_controller_components();
            configure_service_layer_components();
            configure_the_user_commands();
        }

        static void configure_the_user_commands()
        {
            add_factory<ViewTheMainDepartments>(() => new ViewTheMainDepartments(Container.fetch.an<IProvideInformationAboutTheStore>(),Container.fetch.an<IDisplayInformation>()));
            add_factory<ViewTheDepartmentsInADepartment>(() => new ViewTheDepartmentsInADepartment(Container.fetch.an<IProvideInformationAboutTheStore>(),Container.fetch.an<IDisplayInformation>()));
            add_factory<ViewTheProductsInADepartment>(() => new ViewTheProductsInADepartment(Container.fetch.an<IProvideInformationAboutTheStore>(),Container.fetch.an<IDisplayInformation>()));
        }

        static void configure_service_layer_components()
        {
            add_instance<IProvideInformationAboutTheStore>(Stub.with<StubStoreDirectory>());
        }

        static void configure_the_container()
        {
            IFetchDependencies container_facade =
                new ContainerFacade(new DependencyFactories(all_the_factories,
                                                            Stub.with<StubMissingDependencyFactory>().create));
            ContainerFacadeResolver resolver = () => container_facade;
            Container.facade_resolver = resolver;
        }

        static void configure_the_front_controller_components()
        {
            add_instance<ICreateRequests>(Stub.with<StubRequestFactory>());
            add_factory<IProcessRequests>(() =>  new FrontController(Container.fetch.an<IFindCommandsThatCanProcessRequests>()));
            add_instance<IEnumerable<IProcessOneSpecificTypeOfRequest>>(Stub.with<StubSetOfCommands>());
            add_factory<IDisplayInformation>(() => new WebFormReportEngine(Container.fetch.an<ICreateTemplateInstances>(),
                Container.fetch.an<GetTheCurrentlyExecutingContext>()));
            add_factory<ICreateTemplateInstances>(() => new ASPXTemplateFactory(Container.fetch.an<IFindPathsToTemplates>(),
                Container.fetch.an<PageFactory>()));
            add_instance<IFindPathsToTemplates>(Stub.with<StubAspxPathRegistry>());
            add_factory<IFindCommandsThatCanProcessRequests>(() => new CommandRegistry(Container.fetch.an<IEnumerable<IProcessOneSpecificTypeOfRequest>>(),
                Stub.with<StubMissingCommand>()));
            
            add_instance<GetTheCurrentlyExecutingContext>(() => HttpContext.Current);
            add_instance<PageFactory>(BuildManager.CreateInstanceFromVirtualPath);
        }

        static void add_instance<Contract>(Contract instance)
        {
            add_factory<Contract>(() => instance);
        }

        static void add_factory<Contract>(Func<object> factory)
        {
            all_the_factories.Add(typeof(Contract), new SimpleDependencyFactory(factory));
        }

    }
}
