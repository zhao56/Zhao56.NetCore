using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhao56.Core.TransResponse
{
    public class WebJsonResult
    {
        public string Message { get; set; }
        public object Value { get; set; }
        public string Code { get; set; }
        public WebJsonResult(ServiceResponseModel model)
        {
            this.Message = model.Message;
            this.Value = model.Value;
            this.Code = model.Success?"0":"1";
        }
    }
}
