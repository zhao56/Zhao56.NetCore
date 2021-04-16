using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.Enums;

namespace Zhao56.Core.BaseModel.Orders
{
    public static class OrderExtensions
    {
        public static IOrder<TEntity> ApplyOrderBy<TEntity>(this IOrder<TEntity> order, Expression<Func<TEntity, object>> query, QueryOrderBy sort) where TEntity:BaseEntity
        {
            order.Setstock(query, sort);
            return order;
        }
    }
}
