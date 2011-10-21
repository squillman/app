using System;
using System.Reflection;

namespace app.infrastructure.containers
{
    public class GreediestContructorPicker:IChooseTheConstructorForAType
    {
        public ConstructorInfo get_the_applicable_constructor_on(Type type)
        {
            throw new NotImplementedException();
        }
    }
}