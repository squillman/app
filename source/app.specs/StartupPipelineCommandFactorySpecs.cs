 using Machine.Specifications;
 using app.infrastructure.containers.simple;
 using app.tasks.startup.pipeline;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace app.specs
{  
    [Subject(typeof(StartupPipelineCommandFactory))]  
    public class StartupPipelineCommandFactorySpecs
    {
        public abstract class concern : Observes<ICreateStartupPipelineCommands,
                                            StartupPipelineCommandFactory>
        {
        
        }

   
        public class when_creating_a_startup_command : concern
        {
            public class and_it_follows_all_the_necessary_conventions:when_creating_a_startup_command
            {
                Establish c = () =>
                {
                    the_registration_facility = depends.on<IRegisterComponentsIntoTheContainer>();
                };

                Because b = () =>
                    result = sut.create_command_of(typeof(TheCommand));

                It should_return_the_command_instance_with_its_required_registration_service = () =>
                {
                    var command = result.ShouldBeAn<TheCommand>();
                    command.registration.ShouldEqual(the_registration_facility);

                };

                static IPlayAPartInApplicationStartUp result;
                static IRegisterComponentsIntoTheContainer the_registration_facility;
            }
                
        }

        public class TheCommand:IPlayAPartInApplicationStartUp
        {
            public IRegisterComponentsIntoTheContainer registration;

            public TheCommand(IRegisterComponentsIntoTheContainer registration)
            {
                this.registration = registration;
            }

            public void run()
            {
                throw new System.NotImplementedException();
            }
    }
    }

}
