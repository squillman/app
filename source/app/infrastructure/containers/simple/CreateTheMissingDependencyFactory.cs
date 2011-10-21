using System;

namespace app.infrastructure.containers.simple
{
    public delegate ICreateASingleDependency CreateTheMissingDependencyFactory(Type type_that_has_no_factory);
}