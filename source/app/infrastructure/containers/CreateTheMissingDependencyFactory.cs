using System;

namespace app.infrastructure.containers
{
    public delegate ICreateASingleDependency CreateTheMissingDependencyFactory(Type type_that_has_no_factory);
}