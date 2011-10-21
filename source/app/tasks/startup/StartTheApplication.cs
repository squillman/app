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
            IFetchDependencies container_facade =
                new ContainerFacade(new DependencyFactories(all_the_factories,
                                                            Stub.with<StubMissingDependencyFactory>().create));
            ContainerFacadeResolver resolver = () => container_facade;
            Container.facade_resolver = resolver;

            populate_dependency_factories();
        }

        static void populate_dependency_factories()
        {
            all_the_factories.Add(typeof(IProvideInformationAboutTheStore),new SimpleDependencyFactory(() => Stub.with<StubStoreDirectory>()));
            all_the_factories.Add(typeof(ICreateRequests),
                                  new SimpleDependencyFactory(() => Stub.with<StubRequestFactory>()));
            all_the_factories.Add(typeof(IProcessRequests),
                                  new SimpleDependencyFactory(
                                      () =>
                                          new FrontController(Container.fetch.an<IFindCommandsThatCanProcessRequests>())));
            all_the_factories.Add(typeof(IEnumerable<IProcessOneSpecificTypeOfRequest>),
                                  new SimpleDependencyFactory(() => Stub.with<StubSetOfCommands>()));
            all_the_factories.Add(typeof(IDisplayInformation),
                                  new SimpleDependencyFactory(
                                      () => new WebFormReportEngine(Container.fetch.an<ICreateTemplateInstances>(),
                                                                    Container.fetch.an<GetTheCurrentlyExecutingContext>())));
            all_the_factories.Add(typeof(IFindCommandsThatCanProcessRequests),
                                  new SimpleDependencyFactory(
                                      () =>
                                          new CommandRegistry(
                                          Container.fetch.an<IEnumerable<IProcessOneSpecificTypeOfRequest>>(),
                                          Stub.with<StubMissingCommand>())));
            all_the_factories.Add(typeof(ICreateTemplateInstances),
                                  new SimpleDependencyFactory(
                                      () => new ASPXTemplateFactory(Container.fetch.an<IFindPathsToTemplates>(),
                                                                    Container.fetch.an<PageFactory>())));

            all_the_factories.Add(typeof(IFindPathsToTemplates),
                                  new SimpleDependencyFactory(() => Stub.with<StubAspxPathRegistry>()));
            all_the_factories.Add(typeof(ViewTheMainDepartments),
                                  new SimpleDependencyFactory(
                                      () =>
                                          new ViewTheMainDepartments(
                                          Container.fetch.an<IProvideInformationAboutTheStore>(),
                                          Container.fetch.an<IDisplayInformation>())));

            all_the_factories.Add(typeof(ViewTheDepartmentsInADepartment),
                                  new SimpleDependencyFactory(
                                      () =>
                                          new ViewTheDepartmentsInADepartment(
                                          Container.fetch.an<IProvideInformationAboutTheStore>(),
                                          Container.fetch.an<IDisplayInformation>())));

            all_the_factories.Add(typeof(ViewTheProductsInADepartment),
                                  new SimpleDependencyFactory(
                                      () =>
                                          new ViewTheProductsInADepartment(
                                          Container.fetch.an<IProvideInformationAboutTheStore>(),
                                          Container.fetch.an<IDisplayInformation>())));
            PageFactory factory = BuildManager.CreateInstanceFromVirtualPath;
            GetTheCurrentlyExecutingContext current_context = () => HttpContext.Current;

            all_the_factories.Add(typeof(PageFactory), new SimpleDependencyFactory(() => factory));
            all_the_factories.Add(typeof(GetTheCurrentlyExecutingContext),
                                  new SimpleDependencyFactory(() => current_context));
        }
    }
}