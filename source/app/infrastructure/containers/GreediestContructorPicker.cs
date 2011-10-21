using System;
using System.Reflection;

namespace app.infrastructure.containers
{
    public class GreediestContructorPicker:IChooseTheConstructorForAType
    {
        public ConstructorInfo get_the_applicable_constructor_on(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors();
            ConstructorInfo greediest_constructor = constructors[0];
            foreach (var constructor in constructors)
            {
                if (constructor.GetParameters().GetUpperBound(0) > greediest_constructor.GetParameters().GetUpperBound(0))
                    greediest_constructor = constructor;
            }
            return greediest_constructor;
        }
    }
}