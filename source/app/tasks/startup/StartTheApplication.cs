using System;
using System.Collections.Generic;
using app.infrastructure.containers;
using app.infrastructure.containers.simple;
using app.infrastructure.containers.simple.stubs;
using app.web.infrastructure;
using app.web.infrastructure.stubs;

namespace app.tasks.startup
{
    public class StartTheApplication
    {
        static IDictionary<Type, ICreateASingleDependency> all_the_factories = new Dictionary<Type, ICreateASingleDependency>();

        public static void run()
        {
            IFetchDependencies container_facade = new ContainerFacade(new DependencyFactories(all_the_factories,Stub.with<StubMissingDependencyFactory>().create);
            ContainerFacadeResolver resolver = () => container_facade;
            Container.facade_resolver = resolver;


            populate_dependency_factories();

        }

        static void populate_dependency_factories()
        {
            all_the_factories.Add(typeof(IProcessRequests),new SimpleDependencyFactory(() => new FrontController(Container.fetch.an<IFindCommandsThatCanProcessRequests>())));
            all_the_factories.Add(typeof(IFindCommandsThatCanProcessRequests),new SimpleDependencyFactory(() => new CommandRegistry(Container.fetch.an<IEnumerable<IProcessOneSpecificTypeOfRequest>>(),
                Stub.with<StubMissingCommand>())));
        }
    }
}