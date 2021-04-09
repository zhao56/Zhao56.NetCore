using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhao56.Core.BaseModel
{
    public interface IBaseEntity
    {
        DTO ConvertEntityToDto<DTO>() where DTO : DTOBase, new();
    }
}
