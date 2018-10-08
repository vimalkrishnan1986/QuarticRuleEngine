using Quartic.Engine.Business.Common;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Quartic.Engine.Business.Models;
using Quartic.Engine.Business.Core;
using Quartic.Engine.Logging;

namespace Quartic.Engine.Business.Services
{
    public sealed class RuleEngineService : IRuleEngineService
    {
        private readonly ILoggingService _loggingService;
        public RuleEngineService(ILoggingService loggingService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }

        public async Task<List<Message>> Apply(List<Message> messages, int ruleId)
        {
            try
            {
                _loggingService.Log($"Rule {ruleId} has been recieved ");
                IMessageExpression messageExpression = MessageExppression.GetInstance(_loggingService, ruleId);
                _loggingService.Log($"Expression has been created for ruleId {ruleId}");
                IEngine engine = RuleEngine.Getnstance(_loggingService, messageExpression);
                _loggingService.Log($"Rule engine has nee created for ruleId {ruleId}");
                _loggingService.Log($"Applying the expression obatianed for ruleId {ruleId}");
                return await engine.Apply(messages);
            }
            catch (Exception ex)
            {
                _loggingService.Log(ex);
                throw;
            }
        }
    }
}
