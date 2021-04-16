using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhao56.Core.TransResponse
{
    public class ResponseBase
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回值 可被继承
        /// </summary>
        public virtual object Result { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public string Code { get; set; }

        public ResponseBase()
        {
            IsSuccess = true;
        }
        public ResponseBase(Object o)
        {
            IsSuccess = true;
            Result = o;
        }
    }
}
