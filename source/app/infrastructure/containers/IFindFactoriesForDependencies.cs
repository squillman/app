using System;

namespace app.infrastructure.containers
{
    public interface IFindFactoriesForDependencies
    {
        ICreateASingleDependency get_the_factory_that_can_create(Type dependency_type);
    }
}