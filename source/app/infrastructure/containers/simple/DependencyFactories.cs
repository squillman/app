using System;
using System.Collections.Generic;

namespace app.infrastructure.containers.simple
{
    public class DependencyFactories : IFindFactoriesForDependencies
    {
        IDictionary<Type, ICreateASingleDependency> factories;
        CreateTheMissingDependencyFactory special_case;

        public DependencyFactories(IDictionary<Type, ICreateASingleDependency> factories, CreateTheMissingDependencyFactory special_case)
        {
            this.factories = factories;
            this.special_case = special_case;
        }

        public ICreateASingleDependency get_the_factory_that_can_create(Type dependency_type)
        {
            if (factories.ContainsKey(dependency_type)) return factories[dependency_type];

            return special_case(dependency_type);
        }
    }
}