using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.BaseModel;
using Zhao56.Core.TransResponse;

namespace Zhao56.Core.BaseProvider
{
    public abstract class BaseService<T, TRepository>where T: BaseEntity where TRepository: IRepository<T>
    {
        protected IRepository<T> repository;
        private ServiceResponseModel Response { get; set; }

        public BaseService() { }
        public BaseService(TRepository repository) {
            Response = new ServiceResponseModel(true);
            this.repository = repository;
        }


        public virtual ServiceResponseModel GetPageData(PageDataOptions pageData)
        {
            ServiceResponseModel result = new ServiceResponseModel();
            var w = repository.GetAllAsync();
            return result;
        }

    }
}
