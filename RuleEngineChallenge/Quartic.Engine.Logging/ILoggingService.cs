using System;
using System.Collections.Generic;
using System.Text;

namespace Quartic.Engine.Logging
{
    public interface ILoggingService
    {
        void Log(Exception exception);
        void Log(string message);
    }
}
