using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.BaseModel;
using Zhao56.Core.Paging;

namespace Zhao56.Core.TransResponse
{
    public class PageResponse<TEntity> :ResponseBase where TEntity : BaseEntity
    {
        public new IPagedList<TEntity> Result { get; set; }
    }
}
