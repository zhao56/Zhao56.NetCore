using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.Enums;

namespace Zhao56.Core.BaseModel.Orders
{
    public  class Order<TEntity>: IOrder<TEntity> where TEntity:BaseEntity
    {
        private static Dictionary<Expression<Func<TEntity, object>>, QueryOrderBy> stock = new Dictionary<Expression<Func<TEntity, object>>, QueryOrderBy>();

        public Dictionary<Expression<Func<TEntity, object>>, QueryOrderBy> Getstock()
        {
            return stock;
        }

        public void Setstock(Expression<Func<TEntity, object>>order, QueryOrderBy sort)
        {
            stock.Add(order, sort);
        }
    }
}
