using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Builder.Entities;
using Zhao56.Builder.IRepositories.core;
using Zhao56.Builder.IServices;
using Zhao56.Builder.Services.Dto;
using Zhao56.Core.BaseProvider;

namespace Zhao56.Builder.Services
{
    public partial class SysTableInfoService:BaseService<sys_tableInfo, SysTableInfoDto, ISysTableInfoRepository>, ISysTableInfoService, IDependency
    {
        public SysTableInfoService(ISysTableInfoRepository repository) : base(repository)
        {

        }
    }
}
