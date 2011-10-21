using System;
using System.Linq;

namespace app.infrastructure.containers.simple
{
    public class AutomaticallyWiringDependencyFactory : ICreateASingleDependency
    {
        public IFetchDependencies container;
        public Type dependency_type;
        public IChooseTheConstructorForAType constructor_picker;

        public AutomaticallyWiringDependencyFactory(IFetchDependencies container, Type dependencyType, IChooseTheConstructorForAType constructor_picker)
        {
            this.container = container;
            dependency_type = dependencyType;
            this.constructor_picker = constructor_picker;
        }

        public object create()
        {
            var ctor = constructor_picker.get_the_applicable_constructor_on(dependency_type);
            var parameters = ctor.GetParameters().Select(x => container.an(x.ParameterType));
            return ctor.Invoke(parameters.ToArray());
        }
    }
}