using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhao56.Builder.IServices;
using Zhao56.Core.BaseModel;
using Zhao56.Core.Controllers.Base;
using Zhao56.Core.TransResponse;

namespace Zhao56.WebApi.Controllers
{
    [Route("/api/Builder")]
    public class BuilderController : ApiBaseController<ISysTableInfoService>
    {
        public BuilderController(ISysTableInfoService service):base(service)
        {

        }
    }
}
