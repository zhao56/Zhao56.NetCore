using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.Paging;

namespace Zhao56.Core.BaseModel.Paging
{
    public class PagedList<TEntity> : IPagedList<TEntity> where TEntity : BaseEntity
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public List<TEntity> Entities { get; set; } = new List<TEntity>();
    }
}
