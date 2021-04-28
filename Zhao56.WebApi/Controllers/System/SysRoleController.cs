using Zhao56.System.IServices;
using Zhao56.Core.Controllers.Base;
using Microsoft.AspNetCore.Components;

namespace Zhao56.System.Controllers
{
    [Route("/api/SysRole")]
    public partial class SysRoleController : ApiBaseController<ISysRoleService>
    {
        public SysRoleController(ISysRoleService service) : base(service)
        {

        }
    }
}
