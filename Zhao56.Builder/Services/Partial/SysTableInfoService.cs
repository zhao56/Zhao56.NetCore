using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Zhao56.Builder.Entities;
using Zhao56.Core;
using Zhao56.Core.BaseModel;
using Zhao56.Core.Const;
using Zhao56.Core.TransResponse;
using Zhao56.Core.Utils;

namespace Zhao56.Builder.Services
{
    public partial class SysTableInfoService
    {

        public ResponseBase CreateEntity(sys_tableInfo sysTableInfo)
        {
            if (sysTableInfo==null|| sysTableInfo.TableColumns==null|| sysTableInfo.TableColumns.Count==0)
            {
                return Err("生成实体入参不完整");
            }
            if (string.IsNullOrEmpty(sysTableInfo.TableTrueName))
            {
                return Err("真实表名不能为空");
            }
            string tableName = sysTableInfo.TableName;
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = sysTableInfo.TableTrueName;
            }
            var exist = TableExists(tableName, sysTableInfo.TableTrueName);
            if (!exist.IsSuccess)
            {
                return exist;
            }
            string sql = "";
            //通过配置名查找对应的sql语句
            switch (DBType.Type)
            {
                case (int)EFDbCurrentTypeEnum.MySql:
                    sql = "";
                    break;
                default:
                    break;
            }

            return OK();

        }
        private ResponseBase TableExists(string table_name, string tableTrueName)
        {
            if (string.IsNullOrEmpty(table_name)|| string.IsNullOrEmpty(tableTrueName))
            {
                return Err("不能创建表名为空的表");
            }
            var compilationLibrary = DependencyContext
                    .Default
                    .CompileLibraries
                    .Where(x => !x.Serviceable && x.Type != "package" && x.Type == "project");
            foreach (var _compilation in compilationLibrary)
            {
                //加载指定类
                foreach (var t in AssemblyLoadContext.Default
                .LoadFromAssemblyName(new AssemblyName(_compilation.Name))
                .GetTypes()
                .Where(x =>
                    x.GetTypeInfo().BaseType != null
                    && x.BaseType == (typeof(BaseEntity))))
                {
                    if (t.Name.ToUpper() == table_name.ToUpper())
                    {
                        return Err($"别名实体{table_name}已创建");
                    }
                    else
                    {
                        var tableAttr = t.GetCustomAttribute<TableAttribute>();
                        if (tableAttr != null && tableAttr.Name.ToUpper() == tableTrueName.ToUpper())
                        {
                            return Err($"真实表{tableTrueName}已创建，别名为{t.Name}");
                        }
                    }
                }  
            }
            return OK();
        }

        /// <summary>
        /// 生成文件IRepositories Repositories IServices controller
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="foldername">文件夹分组</param>
        /// <returns></returns>
        public ResponseBase CreateServices(string tableName, string foldername)
        {
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(foldername))
            {
                return Err($"表名、命名空间、文件夹分组不能为空");
            }
            var table = repository.FindAsyncFirst(t => t.TableName == tableName).Result;
            if (table == null)
            {
                return Err($"没查到{tableName}表相关的信息");
            }
            string nameSpace = table.Namespace;
            //查找项目根文件夹路径
            var programDirectory = PathHelper.GetProgramDirectoryInfo();
            //获取api路径
            var apiDirectory = PathHelper.GetContentRootDirectoryInfo();
            string programPath = programDirectory?.FullName;
            string contentRootPath = apiDirectory?.FullName;
            //跟文件夹名称
            string programName = programDirectory?.Name;
            //api名称
            string contentName = apiDirectory?.Name;
            if (programPath==null)
            {
                return Err("找不到项目根文件");
            }
            if (contentRootPath == null)
            {
                return Err("找不到API路径");
            }
            //找到工程名
            var startName = contentName.Substring(0, contentName.IndexOf('.'));
            string bigHumpTableName = StringHelper.String2BigHump(tableName);
            string content = FileHelper.ReadFile($"{contentRootPath}\\Template\\IRepositorys\\IRepositorieTemplate.html").Replace("{Namespace}", nameSpace).Replace("{TableName}", tableName).Replace("{BigHumpTableName}", bigHumpTableName).Replace("{StartName}", startName) ;
            //生成IRepositories
            FileHelper.WriteFile($"{programName}\\{nameSpace}\\IRepositories\\{foldername}\\" 
                          , $"I{bigHumpTableName}Repository.cs", content);
            //生成 Repositories
            content = FileHelper.ReadFile(programPath + "\\Template\\Repositories\\RepositorieTemplate.html").Replace("{Namespace}", nameSpace).Replace("{TableName}", tableName).Replace("{BigHumpTableName}", bigHumpTableName).Replace("{StartName}", startName);
            FileHelper.WriteFile( $"{programName}\\{nameSpace}\\Repositories\\{foldername}\\"
                          , $"{bigHumpTableName}Repository.cs", content);
            //生成 IServices
            content = FileHelper.ReadFile(programPath + "\\Template\\IServices\\IServicesTemplate.html").Replace("{Namespace}", nameSpace).Replace("{TableName}", tableName).Replace("{BigHumpTableName}", bigHumpTableName).Replace("{StartName}", startName);
            FileHelper.WriteFile($"{programName}\\{nameSpace}\\IServices\\{foldername}\\"
                          , $"I{bigHumpTableName}Services.cs", content);
            //生成 Services
            content = FileHelper.ReadFile(programPath + "\\Template\\Services\\ServicesTemplate.html").Replace("{Namespace}", nameSpace).Replace("{TableName}", tableName).Replace("{BigHumpTableName}", bigHumpTableName).Replace("{StartName}", startName);
            FileHelper.WriteFile($"{programName}\\{nameSpace}\\Services\\{foldername}\\"
                          , $"{bigHumpTableName}Services.cs", content);
            //生成 controller
            return OK();
        }

    }
}
