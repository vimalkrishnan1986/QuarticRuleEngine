using System;
using System.Linq.Expressions;
using Quartic.Engine.Business.Common;
using Quartic.Engine.Business.Models;
using Quartic.Engine.Logging;
using Quartic.Engine.Data.Repository;
using Quartic.Engine.Data.Contracts;
using Quartic.Engine.Data.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Quartic.Engine.Business.Core
{
    public sealed class MessageExppression : IMessageExpression
    {
        private static Dictionary<int, Func<Message, bool>> _expressionCache;
        private readonly ILoggingService _loggingService;
        private readonly IRepository<RuleExpression> _repository;
        private readonly int _id;


        public Func<Message, bool> Expression
        {
            get
            {
                return CreateExpression(_id);
            }

        }
        private Func<Message, bool> CreateExpression(int id)
        {
            if (_expressionCache == null)
            {
                _loggingService.Log($"Initializing Expression Cache");
                _expressionCache = new Dictionary<int, Func<Message, bool>>();
            }
            _loggingService.Log($"Fetching the expression for ruleid {id}");
            string strExpression = _repository.Get(p => p.Id == _id).Expression;
            if (!_expressionCache.ContainsKey(id))
            {
                _loggingService.Log($"Creating expression from string for ruleId {id} :Recieved expression string {strExpression}");
                var expression = ExpressionParser.CompileRule(JsonConvert.DeserializeObject<FilterExpression>(strExpression));
                _loggingService.Log($"Adding the expression into cache, key {id}");
                _expressionCache.Add(id, expression);
            }
            return _expressionCache[id];
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
