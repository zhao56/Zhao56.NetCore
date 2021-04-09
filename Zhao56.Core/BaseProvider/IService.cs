using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.BaseModel;
using Zhao56.Core.TransResponse;

namespace Zhao56.Core.BaseProvider
{
    public interface IService<T, DTO> where T : BaseEntity where DTO: DTOBase
    {
        ResponseBase GetPageData(PageDataOptions pageData);
    }
}
