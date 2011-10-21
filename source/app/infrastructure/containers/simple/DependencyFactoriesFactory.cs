namespace app.infrastructure.containers.simple
{
    public class DependencyFactoriesFactory : ICreateDependencyFactories
    {
        IFetchDependencies container;
        IChooseTheConstructorForAType constructor_picker;

        public DependencyFactoriesFactory(IFetchDependencies container, IChooseTheConstructorForAType constructor_picker)
        {
            this.container = container;
            this.constructor_picker = constructor_picker;
        }

        public ICreateASingleDependency create_for_instance<Contract>(Contract instance)
        {
            return new SimpleDependencyFactory(() => instance);
        }

        public ICreateASingleDependency create_for_automatic_wiring<Implementation>()
        {
            return new AutomaticallyWiringDependencyFactory(container, typeof(Implementation), constructor_picker);
        }
    }
}