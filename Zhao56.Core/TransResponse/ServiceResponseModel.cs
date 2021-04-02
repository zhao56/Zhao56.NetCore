using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhao56.Core.TransResponse
{
    public class ServiceResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Value { get; set; }
        public string Code { get; set; }
        public ServiceResponseModel(){}
        public ServiceResponseModel(bool Success)
        {
            this.Success = Success;
        }
        public ServiceResponseModel OK()
        {
            this.Success = true;
            return this;
        }
        public static ServiceResponseModel Instance
        {
            get { return new ServiceResponseModel(); }
        }
        public ServiceResponseModel Error(string message = null)
        {
            this.Success = false;
            this.Message = message;
            return this;
        }
    }
}
