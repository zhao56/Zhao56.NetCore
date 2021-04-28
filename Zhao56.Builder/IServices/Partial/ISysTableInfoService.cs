using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.TransResponse;

namespace Zhao56.Builder.IServices
{
    /// <summary>
    /// 防止被生成器给覆盖掉
    /// </summary>
    public partial interface ISysTableInfoService
    {
        ResponseBase CreateServices(string tableName);

    }
}
