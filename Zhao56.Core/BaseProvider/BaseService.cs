using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.BaseModel;
using Zhao56.Core.TransResponse;

namespace Zhao56.Core.BaseProvider
{
    public abstract class BaseService<T,DTO, TRepository>where T: BaseEntity where TRepository: IRepository<T> where DTO : DTOBase, new()
    {
        protected IRepository<T> repository;
        private ResponseBase Response { get; set; }

        public BaseService() { }
        public BaseService(TRepository repository) {
            Response = new ResponseBase();
            this.repository = repository;
        }
        protected ResponseBase Result(Object value)
        {
            Response.IsSuccess = true;
            Response.Value = value;
            return Response;
        }

        public virtual ResponseBase GetPageData(PageDataOptions pageData)
        {
            var list = repository.GetAll();
            var result = new List<DTO>();
            if (list!=null&& list.Count>0)
            {
                foreach (var item in list)
                {
                    result.Add(item.ConvertEntityToDto<DTO>());
                }
            }
            return Result(result);
        }

    }
}
