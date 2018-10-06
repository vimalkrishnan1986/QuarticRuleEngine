using System.Web.Http;
using Quartic.Engine.Logging;
using System;

namespace Quartic.Engine.Api.Controllers
{
    public abstract class QuarticControllerBase : ApiController
    {
        protected ILoggingService LoggingService { get; private set; }

        protected QuarticControllerBase(ILoggingService loggingService)
        {
            LoggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }
        protected void HandleExcpetion(Exception exception)
        {

        }
    }
}
