using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Zhao56.Core.Configuration
{
    public class Connection
    {
        public string DBType { get; set; }
        public string DbConnectionString { get; set; }
    }
    public static class AppSettings
    {
        private static Connection _connection;
        public static void Init(IServiceCollection services, IConfiguration configuration)
        {
            //注册
            services.Configure<Connection>(configuration.GetSection("Connection"));
            //获取赋值
            var builder = services.BuildServiceProvider();
            //GetRequiredService 和 GetService 区别是前者如果没有则异常，后者默认返回空
            _connection = builder.GetRequiredService<Connection>();
        }
    }
}
