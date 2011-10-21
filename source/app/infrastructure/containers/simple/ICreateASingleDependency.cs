namespace app.infrastructure.containers.simple
{
    public interface ICreateASingleDependency
    {
        object create();
    }

    public class SingletonDependencyFactory:ICreateASingleDependency
    {
        ICreateASingleDependency original;
        object cached; 

        public SingletonDependencyFactory(ICreateASingleDependency original)
        {
            this.original = original;
        }

        public object create()
        {
            return cached ?? (cached = original.create());
        }
    }
}