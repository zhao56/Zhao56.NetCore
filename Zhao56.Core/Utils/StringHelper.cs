using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Zhao56.Core.Utils
{
    public class StringHelper
    {
        /// <summary>
        /// 字符串转大驼峰之替换_(下划线)如table_name 不能转换前后带_的
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public static string String2BigHump(string parm)
        {
            if (string.IsNullOrEmpty(parm)) return "";
            parm = parm.Substring(0, 1).ToUpper() + parm.Substring(1);
            Regex reg = new Regex("_([a-z])");
            var matchs =  reg.Matches(parm).Distinct();
            foreach (var match in matchs)
            {
                parm = parm.Replace(match.Value, match.Value.Split("_")[1].ToUpper());
            }
            return parm;
        }
    }
}
