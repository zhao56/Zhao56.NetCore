using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.Enums;

namespace Zhao56.Core.BaseModel.Orders
{
    public interface IOrder<TEntity> where TEntity : BaseEntity
    {
        Dictionary<Expression<Func<TEntity, object>>, QueryOrderBy> Getstock();

        void Setstock(Expression<Func<TEntity, object>> order, QueryOrderBy sort);
    }
}
