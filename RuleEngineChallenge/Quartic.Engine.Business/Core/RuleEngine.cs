using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Quartic.Engine.Business.Common;
using Quartic.Engine.Business.Models;
using Quartic.Engine.Logging;
using System.Linq;

namespace Quartic.Engine.Business.Core
{
    public sealed class RuleEngine : IEngine
    {
        private readonly ILoggingService _loggingService;
        private readonly IMessageExpression _messageExpression;

        private RuleEngine(ILoggingService loggingService, IMessageExpression messageExpression)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
            _messageExpression = messageExpression ?? throw new ArgumentNullException(nameof(messageExpression));

        }

        public async Task<List<Message>> Apply(List<Message> inputs)
        {
            if (_messageExpression.Expression == null)
            {
                throw new ArgumentNullException(nameof(_messageExpression.Expression));
            }
            return await Task.FromResult(inputs.Where(p => _messageExpression.Expression(p)).ToList());
        }

        #region static mmethods

        public static IEngine CreateEngine(ILoggingService loggingService, IMessageExpression messageExpression)
        {
            return new RuleEngine(loggingService, messageExpression);
        }
        #endregion
    }
}
