using System;
using System.Linq.Expressions;
using Quartic.Engine.Business.Common;
using Quartic.Engine.Business.Models;
using Quartic.Engine.Logging;
using Quartic.Engine.Data.Repository;
using Quartic.Engine.Data.Contracts;
using Quartic.Engine.Data.Entities;
using Newtonsoft.Json;
using System.Linq;
namespace Quartic.Engine.Business.Core
{
    public class MessageExppression : IMessageExpression
    {
        private readonly ILoggingService _loggingService;
        private readonly IRepository<RuleExpression> _repository;
        private readonly int _id;

        public Func<Message, bool> Expression
        {
            get
            {
                return CreateExpression(_repository.Get(p => p.Id == _id).Expression);
            }

        }
        private Func<Message, bool> CreateExpression(string strExpression)
        {
            _loggingService.Log($"Creating expression from string for ruleId {_id} :Recieved expression string {strExpression}");
            return ExpressionParser.Parse(JsonConvert.DeserializeObject<FilterExpression>(strExpression));
        }
        private MessageExppression(ILoggingService loggingService, IRepository<RuleExpression> repository, int ruleId)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _id = ruleId;
        }

        public static IMessageExpression GetInstance(ILoggingService loggingService, int ruleId)
        {
            loggingService.Log($"New create instance call has been recieved for ruleId{ruleId}");
            var repository = new RuleRepository();
            return new MessageExppression(loggingService, repository, ruleId);
        }
    }
}
