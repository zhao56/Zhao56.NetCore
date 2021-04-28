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
    /// <summary>
    /// 代码生成器
    /// </summary>
    [Route("/api/Builder")]
    public class BuilderController : ApiBaseController<ISysTableInfoService>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public BuilderController(ISysTableInfoService service):base(service)
        {

        }
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        [Route("CreateServices")]
        [HttpPost]
        public ResponseBase CreateServices(string tableName)
        {
            return _service.CreateServices(tableName);
        }
    }
}
