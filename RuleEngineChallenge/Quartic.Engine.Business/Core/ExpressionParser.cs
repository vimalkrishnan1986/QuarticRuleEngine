
using Quartic.Engine.Business.Enums;
using Quartic.Engine.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quartic.Engine.Business.Core
{
    public static class ExpressionParser
    {
        //This method needs to be modified using reflector and not doing presently due to lack of time
        public static Func<Message, bool> Parse(FilterExpression filterExpression)
        {
            Func<Message, bool> func = null;
            if (filterExpression.Field == "Signal")
            {

                if (filterExpression.Operators == Operators.Equals)
                {
                    func = new Func<Message, bool>(e => e.Signal == filterExpression.Value);
                }
            }
            return func;
        }

    }
}
