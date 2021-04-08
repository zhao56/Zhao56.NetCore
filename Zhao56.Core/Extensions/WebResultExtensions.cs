using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.TransResponse;

namespace Zhao56.Core.Extensions
{
    public static class WebResultExtensions
    {
        public static ServiceResponseModel ServiceResultToWebResult(this Object o)
        {
            if(o is ServiceResponseModel)
            {
                return (ServiceResponseModel)o;
            }
            else
            {
                throw new  Exception("service 返回到前端的类型不正确");
            }
        }
    }
}
