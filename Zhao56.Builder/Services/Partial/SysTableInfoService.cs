using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using Zhao56.Builder.Entities;
using Zhao56.Builder.IRepositories.core;
using Zhao56.Core;
using Zhao56.Core.BaseModel;
using Zhao56.Core.Const;
using Zhao56.Core.TransResponse;
using Zhao56.Core.Utils;

namespace Zhao56.Builder.Services
{
    public partial class SysTableInfoService
    {
        ISysTableColumnRepository _columRepository;
        public string GetCSharpTypeFromDbType(string type)
        {
            switch (type.ToUpper())
            {
                case "CHAR":
                case "NCHAR":
                case "VARCHAR2":
                case "NVARCHAR2":
                    return "string";
                case "INTEGER":
                    return "int";
                case "NUMBER":
                    return "decimal";
                case "FLOAT":
                    return "double";
                case "LONG":
                case "CLOB":
                case "NCLOB":
                    return "string";
                case "BLOB":
                    return "byte[]";
                case "DATE":
                case "TIMESTAMP":
                    return "DateTime";
                default:
                    return "string";
            }
        }
        public SysTableInfoService(ISysTableInfoRepository repository, ISysTableColumnRepository columRepository) : base(repository)
        {
            _columRepository = columRepository;
        }
        public ResponseBase CheckEntityAndGetColum(sys_tableInfo sysTableInfo,ref string attributeBuilderString)
        {
            if (sysTableInfo==null)
            {
                return Err("生成实体入参不完整");
            }
            if (string.IsNullOrEmpty(sysTableInfo.TableName))
            {
                return Err("表名不能为空");
            }
            string tableName = sysTableInfo.TableName;
            var exist = TableExists(tableName, sysTableInfo.TableTrueName);
            if (!exist.IsSuccess)
            {
                return exist;
            }
            var columList = _columRepository.FindAsync(t => t.Table_Id == sysTableInfo.Table_Id).Result;
            if (columList == null)
            {
                return Err($"没查到{tableName}表相关的列信息");
            }
            StringBuilder attributeBuilder = new StringBuilder();
            foreach (var column in columList)
            {
                //备注
                attributeBuilder.Append("/// <summary>");
                attributeBuilder.Append("\r\n");
                attributeBuilder.Append("        ///" + column.ColumnCnName + "");
                attributeBuilder.Append("\r\n");
                attributeBuilder.Append("        /// </summary>");
                attributeBuilder.Append("\r\n");
                column.ColumnType = (column.ColumnType ?? "").Trim();
                if (column.IsKey == 1)
                {
                    attributeBuilder.Append("        [Key]").Append("\r\n"); ;
                }

                attributeBuilder.Append("       public " + GetCSharpTypeFromDbType(column.ColumnType) + " " + column.ColumnName + " { get; set; }");
                attributeBuilder.Append("\r\n\r\n       ");
            }
            attributeBuilderString = attributeBuilder.ToString();
            return OK();

        }
        private ResponseBase TableExists(string table_name, string tableTrueName)
        {
            if (string.IsNullOrEmpty(table_name))
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
                        if (!string.IsNullOrEmpty(tableTrueName) &&tableAttr != null && tableAttr.Name.ToUpper() == tableTrueName.ToUpper())
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
        public ResponseBase CreateServices(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return Err($"表名不能为空");
            }
            var table = repository.FindAsyncFirst(t => t.TableName == tableName).Result;
            if (table == null)
            {
                return Err($"没查到{tableName}表相关的信息");
            }
            string nameSpace = table.Namespace;
            if (string.IsNullOrEmpty(nameSpace))
            {
                return Err($"表设置中命名空间不能为空");
            }
            string foldername = table.FolderName;
            if (string.IsNullOrEmpty(foldername))
            {
                return Err($"表设置中系统项目文件夹不能为空");
            }
            //查找项目根文件夹路径
            var programDirectory = PathHelper.GetProgramDirectoryInfo();
            //获取api路径
            var apiDirectory = PathHelper.GetContentRootDirectoryInfo();
            string programPath = programDirectory?.FullName;
            string contentRootPath = apiDirectory?.FullName;
            //跟文件夹名称
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
            tableName = tableName.ToLower();
            //生成entity
            string attributeBuilderString = null;
            var checkResult = CheckEntityAndGetColum(table, ref attributeBuilderString);
            if (!checkResult.IsSuccess)
            {
                return checkResult;
            }
            string content = FileHelper.ReadFile($"{contentRootPath}\\Template\\Entity\\EntityTemplate.html").Replace("{Namespace}", nameSpace).Replace("{TableName}", tableName).Replace("{TableTrueName}", table.TableTrueName).Replace("{AttributeList}", attributeBuilderString).Replace("{StartName}", startName);
            FileHelper.WriteFile($"{programPath}\\{nameSpace}\\Entities\\{foldername}\\"
                          , $"{tableName}.cs", content);
            //生成IRepositories
            content = FileHelper.ReadFile($"{contentRootPath}\\Template\\IRepositories\\IRepositorieTemplate.html").Replace("{Namespace}", nameSpace).Replace("{TableName}", tableName).Replace("{BigHumpTableName}", bigHumpTableName).Replace("{StartName}", startName);
            FileHelper.WriteFile($"{programPath}\\{nameSpace}\\IRepositories\\{foldername}\\"
                          , $"I{bigHumpTableName}Repository.cs", content);
            //生成 Repositories
            content = FileHelper.ReadFile($"{contentRootPath}\\Template\\Repositories\\RepositorieTemplate.html").Replace("{Namespace}", nameSpace).Replace("{TableName}", tableName).Replace("{BigHumpTableName}", bigHumpTableName).Replace("{StartName}", startName);
            FileHelper.WriteFile($"{programPath}\\{nameSpace}\\Repositories\\{foldername}\\"
                          , $"{bigHumpTableName}Repository.cs", content);
            //生成 IServices
            content = FileHelper.ReadFile($"{contentRootPath}\\Template\\IServices\\IServicesTemplate.html").Replace("{Namespace}", nameSpace).Replace("{TableName}", tableName).Replace("{BigHumpTableName}", bigHumpTableName).Replace("{StartName}", startName);
            FileHelper.WriteFile($"{programPath}\\{nameSpace}\\IServices\\{foldername}\\"
                          , $"I{bigHumpTableName}Services.cs", content);
            //生成 Services
            content = FileHelper.ReadFile($"{contentRootPath}\\Template\\Services\\ServicesTemplate.html").Replace("{Namespace}", nameSpace).Replace("{TableName}", tableName).Replace("{BigHumpTableName}", bigHumpTableName).Replace("{StartName}", startName);
            FileHelper.WriteFile($"{programPath}\\{nameSpace}\\Services\\{foldername}\\"
                          , $"{bigHumpTableName}Services.cs", content);
            //生成 controller
            content = FileHelper.ReadFile($"{contentRootPath}\\Template\\Controllers\\ControllerTemplate.html").Replace("{Namespace}", nameSpace).Replace("{BigHumpTableName}", bigHumpTableName).Replace("{StartName}", startName);
            FileHelper.WriteFile($"{contentRootPath}\\Controllers\\{foldername}\\"
                          , $"{bigHumpTableName}Controller.cs", content);
            return OK();
        }

    }
}
