
using System;
using System.Collections.Generic;
using app.infrastructure.containers.simple;
using app.tasks.startup;

public class SingletonCreator : Dictionary<Type, ICreateASingleDependency>,
                                                 IRegisterComponentsIntoTheContainer
{
    private ContainerRegistrationFacility original;
    private ICreateDependencyFactories dependency_factories_factory;

    public SingletonCreator(ICreateDependencyFactories dependency_factories_factory,ContainerRegistrationFacility original)
    {
        this.original = original;
        this.dependency_factories_factory = dependency_factories_factory;
    }

    public void register_instance<Contract>(Contract instance)
    {
        original.register<Contract>();
    }

    public void register<Contract, Implementation>() where Implementation : Contract
    {
        original.register<Contract,Implementation>();
    }

    public void register<Contract>()
    {
        original.register<Contract>();
    }
}