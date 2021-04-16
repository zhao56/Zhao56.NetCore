using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.BaseModel;
using Zhao56.Core.BaseModel.Orders;
using Zhao56.Core.Enums;

namespace Zhao56.Core.Extensions
{
    public static class EFExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="propertyName">实体字段名称</param>
        /// <param name="propertyValue">实体值</param>
        /// <param name="parameter">参数p实体前方形参如p.name </param>
        /// <param name="expressionType">操作类型</param>
        /// <returns></returns>
        public static Expression CreateConditionExpression(
          this string propertyName,
          object propertyValue,
          ParameterExpression parameter,
          QueryOperatorEnum expressionType)
        {
            if(parameter==null) throw new NotImplementedException("请传入参数");
            Expression key = Expression.Property(parameter, propertyName);
            Expression value = Expression.Constant(propertyValue);
            Expression expression;
            switch (expressionType)
            {
                case QueryOperatorEnum.Contains:
                    expression =  Expression.Call(key, typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), value);
                    break;
                case QueryOperatorEnum.Equal:
                    expression = Expression.Equal(key, Expression.Convert(value, key.Type)); break;
                case QueryOperatorEnum.GreaterThan:
                    expression = Expression.GreaterThan(key, Expression.Convert(value, key.Type)); break;
                case QueryOperatorEnum.GreaterThanOrEqual:
                    expression = Expression.GreaterThanOrEqual(key, Expression.Convert(value, key.Type)); break;
                case QueryOperatorEnum.LessThan:
                    expression = Expression.LessThan(key, Expression.Convert(value, key.Type)); break;
                case QueryOperatorEnum.LessThanOrEqual:
                    expression = Expression.LessThanOrEqual(key, Expression.Convert(value, key.Type)); break;
                case QueryOperatorEnum.NotEqual:
                    expression = Expression.NotEqual(key, Expression.Convert(value, key.Type)); break;
                //case QueryOperatorEnum.In:
                //    return ParaseIn(p, condition);
                //case QueryOperatorEnum.Between:
                //    return ParaseBetween(p, condition);
                default:
                    throw new NotImplementedException("不支持此操作");
            }
            return expression;
        }
        public static Expression CreateOrdersExpression(
          this string propertyName,
          ParameterExpression parameter)
        {
            Expression.Property(parameter, propertyName);
            return Expression.Property(parameter, propertyName);
        }

        internal static QueryOperatorEnum StringToEfOpType(this string opType)
        {
            switch (opType)
            {
                case "like":
                    return QueryOperatorEnum.Contains;
                case "<>":
                    return QueryOperatorEnum.NotEqual;
                case ">":
                    return QueryOperatorEnum.GreaterThan;
                case ">=":
                    return QueryOperatorEnum.GreaterThanOrEqual;
                case "<":
                    return QueryOperatorEnum.LessThan;
                case "<=":
                    return QueryOperatorEnum.LessThanOrEqual;
                case "=":
                    return QueryOperatorEnum.Equal;
                default:
                    throw new Exception("不支持该方法");
            }
        }

        public static IOrderedQueryable<T> OrderExpressionToOrderedQueryable<T>(this IQueryable<T> query, IOrder<T> order) where T :BaseEntity
        {
            var orderSelector = order.Getstock();
            if (orderSelector == null || orderSelector.Count() == 0)
            {
                return query.OrderBy(t=>true);
            }else if (orderSelector.Values.Count == 1)
            {
                var orderExpression = orderSelector.First();
                if (orderExpression.Value == QueryOrderBy.Asc)
                {
                    return query.OrderBy(orderExpression.Key);
                }
                else
                {
                    return query.OrderByDescending(orderExpression.Key);
                }
            }
            else
            {
                IOrderedQueryable<T> orderQuery = null;
                var firstOrderExpression = orderSelector.First();
                if (firstOrderExpression.Value == QueryOrderBy.Asc)
                {
                    orderQuery = query.OrderBy(firstOrderExpression.Key);
                }
                else
                {
                    orderQuery = query.OrderByDescending(firstOrderExpression.Key);
                }
                foreach (var orderExpression in orderSelector)
                {
                    if (orderExpression.Value == QueryOrderBy.Asc)
                    {
                        orderQuery = orderQuery.OrderBy(orderExpression.Key);
                    }
                    else
                    {
                        orderQuery = orderQuery.OrderByDescending(orderExpression.Key);
                    }
                }
                return orderQuery;
            }
        }
    }
}
