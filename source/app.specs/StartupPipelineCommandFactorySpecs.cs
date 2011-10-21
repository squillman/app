using System;
using System.Collections.Generic;
using Machine.Specifications;
using app.infrastructure.containers.simple;
using app.tasks.startup;
using app.tasks.startup.pipeline;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;

namespace app.specs
{
    [Subject(typeof(StartupPipelineCommandFactory))]
    public class StartupPipelineCommandFactorySpecs
    {
        public abstract class concern : Observes<ICreateStartupPipelineCommands,
                                            StartupPipelineCommandFactory>
        {
        }

        public class SomeRegistration : Dictionary<Type, ICreateASingleDependency>, IRegisterComponentsIntoTheContainer
        {
            public void register_instance<Contract>(Contract instance)
            {
            }

            public void register<Contract, Implementation>() where Implementation : Contract
            {
            }

            public void register<Contract>()
            {
            }
        }

        public class when_creating_a_startup_command : concern
        {
            public class and_it_follows_all_the_necessary_conventions : when_creating_a_startup_command
            {
                Establish c = () =>
                {
                    the_registration_facility = depends.on<IRegisterComponentsIntoTheContainer>(new SomeRegistration());
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

            public class and_it_does_not_follow_the_conventions : when_creating_a_startup_command
            {
                Establish c = () =>
                {
                    the_registration_facility = depends.on<IRegisterComponentsIntoTheContainer>(new SomeRegistration());
                };

                Because b = () =>
                    spec.catch_exception(() => sut.create_command_of(typeof(NotAStartupCommand)));

                It should_throw_startup_command_policy_violated_exception_with_the_correct_information = () =>
                {
                    var item = spec.exception_thrown.ShouldBeAn<StartupCommandPolicyViolationException>();
                    item.command_not_following_the_spec.ShouldEqual(typeof(NotAStartupCommand));
                    item.InnerException.ShouldNotBeNull();
                };

                static IRegisterComponentsIntoTheContainer the_registration_facility;
            }
        }

        public class NotAStartupCommand : IPlayAPartInApplicationStartUp
        {
            public void run()
            {
            }
        }

        public class TheCommand : IPlayAPartInApplicationStartUp
        {
            public IRegisterComponentsIntoTheContainer registration;

            public TheCommand(IRegisterComponentsIntoTheContainer registration)
            {
                this.registration = registration;
            }

            public void run()
            {
            }
        }
    }
}