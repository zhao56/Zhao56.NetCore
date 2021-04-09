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
        protected IServiceBase _service;

        public ApiBaseController(IServiceBase service)
        {
            this._service = service;
        }
        [HttpPost, Route("GetPageData")]
        public virtual ActionResult GetPageData(PageDataOptions options)
        {
            return Content(InvokeService("GetPageData", new object[] { options }).ObjectToJson());
        }

        private object InvokeService(string methodName, object[] parameters)
        {
            return _service.GetType().GetMethod(methodName).Invoke(_service,parameters);
        }
    }
}
