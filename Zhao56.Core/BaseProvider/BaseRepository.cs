using Microsoft.EntityFrameworkCore;
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
using Zhao56.Core.Extensions;
using Zhao56.Core.Paging;

namespace Zhao56.Core.BaseProvider
{
    /// <summary>
    /// Repository基类，用于实现通用方法
    /// </summary>
    public abstract class BaseRepository<TEntity> where TEntity:BaseEntity
    {
        protected DbContext Context { get; }

        protected DbSet<TEntity> Set => Context.Set<TEntity>();

        public BaseRepository(DbContext context)
        {
            Context = context;
        }

        public IPagedList<TEntity> GetPageData(Expression<Func<TEntity, bool>> where, IOrder<TEntity> orderSelector, Pager pager)
        {
            var query = Set.Where(where).OrderExpressionToOrderedQueryable(orderSelector);
            return query.ToPagedList(pager);
        }
        public async Task<TEntity> FindAsyncFirst(Expression<Func<TEntity, bool>> filter)
        {
            return await Set.Where(filter).FirstOrDefaultAsync();
        }

    }
}
