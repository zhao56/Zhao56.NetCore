using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhao56.Builder.IServices;
using Zhao56.Core.BaseModel;
using Zhao56.Core.Controllers.Base;
using Zhao56.Core.Extensions;
using Zhao56.Core.TransResponse;

namespace Zhao56.WebApi.Controllers
{
    [Route("/api/Builder")]
    public class BuilderController : ApiBaseController<ISysTableInfoService>
    {
        public BuilderController(ISysTableInfoService service):base(service)
        {

        }
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="foldername"></param>
        /// <returns></returns>
        [Route("CreateServices")]
        [HttpPost]
        public ActionResult CreateServices(string tableName, string foldername)
        {
            return Content(_service.CreateServices(tableName, foldername).ObjectToJson());
        }
    }
}
