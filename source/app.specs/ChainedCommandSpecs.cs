 using Machine.Specifications;
 using app.infrastructure;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace app.specs
{  
    [Subject(typeof(ChainedCommand))]  
    public class ChainedCommandSpecs
    {
        public abstract class concern : Observes<IEncapsulateABehaviour,
                                            ChainedCommand>
        {
        
        }

   
        public class when_run : concern
        {
            Establish c = () =>
            {
                first = fake.an<IEncapsulateABehaviour>();
                second = fake.an<IEncapsulateABehaviour>();

                sut_factory.create_using(() => new ChainedCommand(first,second));
            };

            Because b = () =>
                sut.run();

            It should_run_both_of_its_commands = () =>
            {
                first.received(x => x.run());
                second.received(x => x.run());
            };

            static IEncapsulateABehaviour first;
            static IEncapsulateABehaviour second;
        }
    }
}
