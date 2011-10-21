using System;
using System.Reflection;
using System.Linq;

namespace app.infrastructure.containers
{
    public class GreediestContructorPicker:IChooseTheConstructorForAType
    {
        public ConstructorInfo get_the_applicable_constructor_on(Type type)
        {
            return type.GetConstructors().OrderByDescending(x => x.GetParameters().Count()).First();
        }
    }
}