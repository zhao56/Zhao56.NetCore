using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.BaseModel;
using Zhao56.Core.BaseModel.Orders;
using Zhao56.Core.BaseModel.Paging;
using Zhao56.Core.EFDbContext;
using Zhao56.Core.Enums;
using Zhao56.Core.Paging;

namespace Zhao56.Core.BaseProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity:BaseEntity
    {
        public IPagedList<TEntity> GetPageData(Expression<Func<TEntity, bool>> where, IOrder<TEntity> orderSelector, Pager pager);

    }
}
