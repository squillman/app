using System;

namespace app.tasks.startup
{
    public class StartupCommandPolicyViolationException:Exception
    {
        public StartupCommandPolicyViolationException(Type command_type, Exception exception):base(string.Empty,exception)
        {
            this.command_not_following_the_spec = command_type;
        }

        public Type command_not_following_the_spec { get; private set; }
    }
}