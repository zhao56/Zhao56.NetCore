using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.BaseModel;

namespace Zhao56.Core.Paging
{
    public interface IPagedList<TEntity> where TEntity: BaseEntity
    {
        int PageIndex { get; set; }

        int PageSize { get; set; }

        int TotalCount { get; set; }

        List<TEntity> Entities { get; set; }
    }
}
