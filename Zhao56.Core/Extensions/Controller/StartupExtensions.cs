using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.BaseProvider;
using Zhao56.Core.Configuration;
using Zhao56.Core.Const;
using Zhao56.Core.EFDbContext;

namespace Zhao56.Core.Extensions.Controller
{
    public static class StartupExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services">容器</param>
        /// <param name="assemblys">程序集</param>
        /// <param name="baseType">基础类/接口</param>
        /// <param name="serviceLifetime">生命周期</param>
        /// <returns></returns>
        public static IServiceCollection BatchRegisterService(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            var compilationLibraries = DependencyContext.Default
                .CompileLibraries
                .Where(x => !x.Serviceable
                && x.Type == "project")
                .ToList();
            var baseType = typeof(IDependency);
            List<Assembly> assemblyList = new List<Assembly>();
            foreach (var compileLibraries in compilationLibraries)
            {
                try
                {
                    assemblyList.Add(AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(compileLibraries.Name)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(compileLibraries.Name + ex.Message);
                }
            }
            List<Type> typeList = new List<Type>();
            foreach (var assembly in assemblyList)
            {
                //获取非接口，非抽象类，非不能继承并且实现指定接口的程序集
                var types = assembly.GetTypes().Where(t => !t.IsAbstract && baseType.IsAssignableFrom(t));
                if (types != null && types.Count() > 0)
                {
                    typeList.AddRange(types);
                }
            }
            if (typeList.Count() == 0)
            {
                return services;
            }
            foreach (var type in typeList)
            {
                var typeInterfaces = type.GetInterfaces().Where(t=>baseType.IsAssignableFrom(t)&&t.Name!= baseType.Name);
                foreach (var typeInterface in typeInterfaces)
                {
                    switch (serviceLifetime)
                    {
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(typeInterface,type);
                            break;
                        case ServiceLifetime.Scoped:
                            services.AddScoped(typeInterface, type);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(typeInterface, type);
                            break;
                        default:
                            break;
                    }
                }
            }
            return services;
        }
        public static readonly ILoggerFactory MyLoggerFactory
= LoggerFactory.Create(builder =>
{
#if DEBUG
    builder.AddConsole();
#endif
});
    public static IServiceCollection AddDbContexts(this IServiceCollection services)
        {
           // services.AddLogging(builder => builder
           //    .AddConsole()
           //    .AddFilter(level => level >= LogLevel.Information)
           //);
            //var loggerFactory = services.BuildServiceProvider().GetService<ILoggerFactory>();
            if (DBType.Type == (int)EFDbCurrentTypeEnum.MySql)
            {
                services.AddDbContext<EFContext>(optionsBuilder => optionsBuilder.UseMySql(AppSettings.DbConnectionString).UseLoggerFactory(MyLoggerFactory));
            }
           
            return services;
        }
    }
}
