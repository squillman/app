using System;
using System.Collections.Generic;

namespace app.infrastructure.containers.simple
{
    public interface IRegisterComponentsIntoTheContainer : IDictionary<Type,ICreateASingleDependency>
    {
        void add_instance<Contract>(Contract instance);
        void add_factory<Contract, Implementation>() where Implementation : Contract;
        void add_factory<Contract>();
    }
}