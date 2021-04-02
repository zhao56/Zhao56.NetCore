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

        private EFContext defaultContext { get; set; }
        public BaseRepository()
        {
            //todo 获取默认的context
        }

        public BaseRepository(EFContext dbContext)
        {
            defaultContext = dbContext ?? throw new Exception("dbContext未实例化");
        }
        public virtual EFContext EFContext
        {
            get { return defaultContext; }
        }
        private DbSet<TEntity> DBSet
        {
            get { return EFContext.Set<TEntity>(); }
        }
    }
}
