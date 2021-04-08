using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.BaseProvider;

namespace Zhao56.Core.EFDbContext
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {

        }

        public DbSet<sys_tableInfo> Microservices { get; set; }

    }
}
