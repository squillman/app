using System;

namespace app.infrastructure.containers.simple
{
    public class AutomaticallyWiringDependencyFactory : ICreateASingleDependency
    {
        private IFetchDependencies container;
        private Type dependency_type;
        private IChooseTheConstructorForAType constructor_picker;

        public AutomaticallyWiringDependencyFactory(IFetchDependencies container, Type dependencyType, IChooseTheConstructorForAType constructorPicker)
        {
            this.container = container;
            dependency_type = dependencyType;
            constructor_picker = constructorPicker;
        }

        public object create()
        {
            return constructor_picker.get_the_applicable_constructor_on(dependency_type);
        }
    }
}