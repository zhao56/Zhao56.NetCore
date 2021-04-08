using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.BaseModel;
using Zhao56.Core.EFDbContext;

namespace Zhao56.Core.BaseProvider
{
    /// <summary>
    /// Repository基类，用于实现通用方法
    /// </summary>
    public abstract class BaseRepository<TEntity> where TEntity:BaseEntity
    {
        protected EFContext Context { get; }

        protected DbSet<TEntity> Set => Context.Set<TEntity>();

        public BaseRepository(EFContext context)
        {
            Context = context;
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await Set.AsNoTracking().ToListAsync();
        }
    }
}
