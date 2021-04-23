using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.IO;
using Zhao56.Core.Const;

namespace Zhao56.Core.Configuration
{
    public class Connection
    {
        public string DBType { get; set; }
        public string DbConnectionString { get; set; }
    }
    public static class AppSettings
    {

        public static string DbConnectionString
        {
            get { return _connection.DbConnectionString; }
        }
        private static Connection _connection;
        public static void Init(IServiceCollection services, IConfiguration configuration)
        {
            //注册
            services.Configure<Connection>(configuration.GetSection("Connection"));
            //获取赋值
            var builder = services.BuildServiceProvider();
            //GetRequiredService 和 GetService 区别是前者如果没有则异常，后者默认返回空
            _connection = builder.GetRequiredService<IOptions<Connection>>().Value;
            int dbType;
            int.TryParse(_connection.DBType,out dbType);
            DBType.Type = dbType;
            if (string.IsNullOrEmpty(_connection.DbConnectionString))
                throw new System.Exception("未配置好数据库默认连接");
        }
    }
}
