using Quartic.Engine.Business.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Quartic.Engine.Business.Models;
using Quartic.Engine.Logging;
using Quartic.Engine.Data.Repository;
using Quartic.Engine.Data.Contracts;
using Quartic.Engine.Data.Entities;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Quartic.Engine.Business.Core
{
    public class ExpressionBuilder : IMessageExpression
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
            XmlSerializer serializer = new XmlSerializer(typeof(Func<Message, bool>));
            return (Func<Message, bool>)serializer.Deserialize(new StringReader(strExpression));
        }
        private ExpressionBuilder(ILoggingService loggingService, IRepository<RuleExpression> repository, int ruleId)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _id = ruleId;
        }
        public static IMessageExpression GetMessageExpression(ILoggingService loggingService, int ruleId)
        {
            var repository = new RuleRepository();
            return new ExpressionBuilder(loggingService, repository, ruleId);
        }
    }
}
