/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 */

using Zhao56.System.Entities;
using Zhao56.System.IRepositories;
using Zhao56.System.IServices;
using Zhao56.Core.BaseProvider;

namespace Zhao56.System.Services
{
    public partial class SysDictionaryService:BaseService<sys_dictionary, ISysDictionaryRepository>
    , ISysDictionaryService, IDependency
    {
        public SysDictionaryService(ISysDictionaryRepository repository) : base(repository)
        {

        }
    }
}

