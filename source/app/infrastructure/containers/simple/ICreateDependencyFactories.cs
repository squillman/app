namespace app.infrastructure.containers.simple
{
    public interface ICreateDependencyFactories
    {
        ICreateASingleDependency create_for_instance<Contract>(Contract instance); 
        ICreateASingleDependency create_for_automatic_wiring<Implementation>(); 
    }
}