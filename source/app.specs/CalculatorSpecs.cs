using Machine.Specifications;

namespace app.specs
{
    public class CalculatorSpecs
    {
        public class when_adding_two_numbers
        {
            private It should_return_the_sum = () => 
                Calculator.when_adding_two_numbers(2,3).ShouldEqual(5);
            private static int result;
        } 
    }
}