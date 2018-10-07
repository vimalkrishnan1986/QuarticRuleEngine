using System;
using System.Linq;
using Quartic.Engine.Business.Models;
namespace Quartic.Engine.Business.Common
{
    public interface IMessageExpression
    {
        Func<Message, bool> Expression { get; }
    }
}
