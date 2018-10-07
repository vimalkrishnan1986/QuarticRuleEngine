using System;
using System.Collections.Generic;
using System.Text;

namespace Quartic.Engine.Logging
{
    public sealed class LoggingService : ILoggingService
    {
        public void Log(Exception exception)
        {
            Log(exception.InnerException?.Message);
        }

        public void Log(string message)
        {
            string formattedMessage = $"{DateTime.UtcNow} : {message} {Environment.NewLine}";
            Console.Write(formattedMessage);
        }
    }
}
