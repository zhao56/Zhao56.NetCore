using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.BaseModel;
using Zhao56.Core.BaseProvider;

namespace Zhao56.Core.EFDbContext
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Type type = null;
            try
            {
                //查询所有继承BaseEntity的实体都需要创建实例化
                var compilationLibrary = DependencyContext
                    .Default
                    .CompileLibraries
                    .Where(x => !x.Serviceable && x.Type != "package" && x.Type == "project");
                foreach (var _compilation in compilationLibrary)
                {
                    //加载指定类
                    AssemblyLoadContext.Default
                    .LoadFromAssemblyName(new AssemblyName(_compilation.Name))
                    .GetTypes()
                    .Where(x =>
                        x.GetTypeInfo().BaseType != null
                        && x.BaseType == (typeof(BaseEntity)))
                        .ToList().ForEach(t =>
                        {
                            modelBuilder.Entity(t);
                        });
                }
                base.OnModelCreating(modelBuilder);
            }
            catch (Exception ex)
            {
                //string mapPath = ($"Log/").MapPath();
                //Utilities.FileHelper.WriteFile(mapPath,
                //    $"syslog_{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt",
                //    type?.Name + "--------" + ex.Message + ex.StackTrace + ex.Source);
                // todo 日志
            }

        }
    }
}
