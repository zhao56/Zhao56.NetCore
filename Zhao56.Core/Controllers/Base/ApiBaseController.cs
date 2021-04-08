using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.BaseModel;
using Zhao56.Core.Extensions;
using Zhao56.Core.TransResponse;

namespace Zhao56.Core.Controllers.Base
{
    /// <summary>
    /// 该控制器用于调用通用的方法
    /// </summary>
    /// <typeparam name="IServiceBase"></typeparam>
    [ApiController]
    public class ApiBaseController<IServiceBase> : Controller
    {
        protected WebJsonResult JsonResult(ServiceResponseModel model)
        {
            return new WebJsonResult(model);
        }
        protected IServiceBase _service;

        public ApiBaseController(IServiceBase service)
        {
            this._service = service;
        }
        [HttpPost,HttpGet, Route("GetPageData")]
        public virtual WebJsonResult GetPageData(PageDataOptions options)
        {
            return JsonResult(InvokeService("GetPageData", new object[] { options }).ServiceResultToWebResult());
        }

        private object InvokeService(string methodName, object[] parameters)
        {
            return _service.GetType().GetMethod(methodName).Invoke(_service,parameters);
        }
    }
}
