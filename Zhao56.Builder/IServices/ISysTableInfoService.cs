using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Builder.Entities;
using Zhao56.Builder.Services.Dto;
using Zhao56.Core.BaseProvider;

namespace Zhao56.Builder.IServices
{
    public partial interface ISysTableInfoService:IService<sys_tableInfo, SysTableInfoDto>, IDependency
    {
    }
}
