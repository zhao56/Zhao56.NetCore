using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhao56.Core.EFDbContext
{
    public class EFContext : DbContext
    {
        public string _connection = null;
        public EFContext():base(){}
        public EFContext(string connection) : base() { this._connection = connection; }
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {

        }
        /// <summary>
        /// 主动回收
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
        }

    }
}
