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
    public partial class SysTableColumnRepository : BaseRepository<sys_table_column>, ISysTableColumnRepository, IDependency
    {
        public SysTableColumnRepository(EFContext dbContext)
        : base(dbContext)
        {

        }
    }
}
