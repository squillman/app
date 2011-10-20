using System;

namespace app.infrastructure.logging
{
    public interface ICreateLoggers
    {
        ILogInformation create_logger_bound_to(Type calling_type);
    }
}