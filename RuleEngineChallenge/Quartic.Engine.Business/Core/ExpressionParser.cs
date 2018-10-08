
using Quartic.Engine.Business.Enums;
using Quartic.Engine.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Quartic.Engine.Business.Core
{
    public static class ExpressionParser
    {
        public static Func<Message, bool> CompileRule(FilterExpression filterExpression)
        {
            var paramUser = Expression.Parameter(typeof(Message));
            Expression expr = BuildExpr(filterExpression, paramUser);
            return Expression.Lambda<Func<Message, bool>>(expr, paramUser).Compile();
        }

        private static Expression BuildExpr(FilterExpression filterExpression, ParameterExpression param)
        {
            var left = MemberExpression.Property(param, filterExpression.Field);
            var tProp = typeof(Message).GetProperty(filterExpression.Field).PropertyType;
            ExpressionType tBinary;
            if (ExpressionType.TryParse(filterExpression.Operators.ToString(), out tBinary))
            {
                var right = Expression.Constant(Convert.ChangeType(filterExpression.Value, tProp));
                return Expression.MakeBinary(tBinary, left, right);
            }

            throw new InvalidOperationException();
        }

    }
}
