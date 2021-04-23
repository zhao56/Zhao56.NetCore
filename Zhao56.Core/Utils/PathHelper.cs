using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.BaseProvider.ServicePath;

namespace Zhao56.Core.Utils
{
    public class PathHelper
    {
        public static DirectoryInfo GetProgramDirectoryInfo()
        {
            string basePath = PathProvider.GetContentRootPath("");
            var currentDictionary = new DirectoryInfo(basePath);
            return FindProjectDirectoryInfo(currentDictionary);
        }
        public static DirectoryInfo GetContentRootDirectoryInfo()
        {
            string basePath = PathProvider.GetContentRootPath("");
            var currentDictionary = new DirectoryInfo(basePath);
            return currentDictionary;
        }
        /// <summary>
        /// 循环查找工程文件夹下级包含.WEBAPI的文件夹
        /// </summary>
        /// <returns></returns>
        private static DirectoryInfo FindProjectDirectoryInfo(DirectoryInfo currentDictionary)
        {
            if (currentDictionary==null)
            {
                return null;
            }
            if (currentDictionary.Exists&& currentDictionary.GetDirectories().Count(t=>t.Name.ToUpper().LastIndexOf(".WEBAPI")!=-1)>0)
            {
                return currentDictionary;
            }
            else
            {
                DirectoryInfo dir = FindProjectDirectoryInfo(currentDictionary.Parent);
                if (dir != null)
                {
                    return dir;
                }
            }

            return null;
        }
    }
}
