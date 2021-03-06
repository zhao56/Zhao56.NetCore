using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Builder.Entities;
using Zhao56.Builder.IRepositories.core;
using Zhao56.Core.BaseProvider;
using Zhao56.Core.EFDbContext;

namespace Zhao56.Builder.Repositories.core
{
    public partial class SysTableInfoRepository : BaseRepository<sys_tableInfo>, ISysTableInfoRepository, IDependency
    {
        public SysTableInfoRepository(EFContext dbContext)
        : base(dbContext)
        {

        }
    }
}
