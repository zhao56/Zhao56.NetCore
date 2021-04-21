using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Builder.Entities;
using Zhao56.Core.BaseProvider;
using Zhao56.Core.TransResponse;

namespace Zhao56.Builder.Services
{
    public partial class SysTableInfoService
    {
        /// <summary>
        /// 生成文件
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="foldername">工程名</param>
        /// <returns></returns>
        public ResponseBase CreateServices(string tableName, string nameSpace, string foldername)
        {
            if (string.IsNullOrEmpty(tableName)||string.IsNullOrEmpty(nameSpace) || string.IsNullOrEmpty(foldername))
            {
                return Err($"表名、命名空间、项目文件名不能为空");
            }
            var table = repository.FindAsyncFirst(t => t.TableName == tableName).Result;
            if (table == null)
            {
                return Err($"没查到{tableName}表相关的信息");
            }
            //查找项目根文件夹
            string basePath = AppContext.BaseDirectory;
            //生成实体
            //生成IRepositories
            //生成 Repositories
            //生成 IServices
            //生成 Services
            //生成 controller
            return null;
        }

    }
}
