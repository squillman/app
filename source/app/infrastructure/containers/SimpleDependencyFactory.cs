using System;

namespace app.infrastructure.containers
{
    public class SimpleDependencyFactory : ICreateASingleDependency
    {
        Func<object> creation_delegate;

        public SimpleDependencyFactory(Func<object> creationDelegate)
        {
            creation_delegate = creationDelegate;
        }

        public object create()
        {
            return creation_delegate();
        }
    }
}