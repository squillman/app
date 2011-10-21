using System;
using System.Collections.Generic;
using app.infrastructure.containers.simple;

namespace app.tasks.startup
{
    public class ContainerRegistrationFacility : Dictionary<Type, ICreateASingleDependency>,
                                                 IRegisterComponentsIntoTheContainer
    {
        ICreateDependencyFactories dependency_factories_factory;

        public ContainerRegistrationFacility(ICreateDependencyFactories dependency_factories_factory)
        {
            this.dependency_factories_factory = dependency_factories_factory;
        }

        public void add_instance<Contract>(Contract instance)
        {
            Add(typeof(Contract), dependency_factories_factory.create_for_instance(instance));
        }

        public void add_factory<Contract, Implementation>() where Implementation : Contract
        {
            Add(typeof(Contract),
                dependency_factories_factory.create_for_automatic_wiring<Implementation>());
        }

        public void add_factory<Contract>()
        {
            add_factory<Contract, Contract>();
        }
    }
}