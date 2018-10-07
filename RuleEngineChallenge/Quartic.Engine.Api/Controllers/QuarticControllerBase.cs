using System;
using Quartic.Engine.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Quartic.Engine.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public abstract class QuarticControllerBase : ControllerBase
    {
        protected ILoggingService LoggingService { get; private set; }

        protected QuarticControllerBase(ILoggingService loggingService)
        {
            LoggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }
        protected void HandleExcpetion(Exception exception)
        {
            LoggingService.Log(exception);

        }
    }
}
