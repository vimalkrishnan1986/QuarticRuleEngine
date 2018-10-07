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
        private IEngine engine;

        public RuleEngineService(ILoggingService loggingService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }

        public async Task<List<Message>> Apply(List<Message> messages, int ruleId)
        {
            IMessageExpression messageExpression = ExpressionBuilder.GetMessageExpression(_loggingService, ruleId);
            engine = RuleEngine.CreateEngine(_loggingService, messageExpression);
            return await engine.Apply(messages);
        }
    }
}
