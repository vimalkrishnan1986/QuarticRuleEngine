using Quartic.Engine.Business.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Quartic.Engine.Logging;
namespace Quartic.Engine.Business.Services
{
    public class RuleBusinessService : IRuleBusinessService
    {
        private readonly ILoggingService _loggingService;

        public RuleBusinessService(ILoggingService loggingService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }

    }
}
