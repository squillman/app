using System;
using System.Reflection;

namespace app.infrastructure.containers
{
    public interface IChooseTheConstructorForAType
    {
        ConstructorInfo get_the_applicable_constructor_on(Type type);
    }
}