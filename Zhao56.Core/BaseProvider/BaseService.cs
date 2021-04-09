using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.BaseModel;
using Zhao56.Core.TransResponse;

namespace Zhao56.Core.BaseProvider
{
    public abstract class BaseService<T,DTO, TRepository>where T: BaseEntity where TRepository: IRepository<T> where DTO : DTOBase
    {
        protected IRepository<T> repository;
        protected ResponseBase Response;
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
        /// <summary>
        /// 通过反射转换对应的DTO
        /// </summary>
        /// <typeparam name="DTO"></typeparam>
        /// <returns></returns>
        protected virtual DTO ConvertEntityToDto(T t)
        {
            DTO dto = Activator.CreateInstance<DTO>();
            if (t == null)
            {
                return default(DTO);
            }
            Type entityType = t.GetType();
            var toProps = dto.GetType().GetProperties();
            foreach (var property in toProps)
            {
                PropertyInfo pi = entityType.GetProperty(property.Name);
                if (pi != null)
                {
                    if (pi.PropertyType == property.PropertyType)
                    {
                        property.SetValue(dto, pi.GetValue(t, null));
                    }
                    else
                    {
                        throw new InvalidOperationException("转换类型失败");
                    }
                }
            }
            return dto;
        }

        protected virtual T ConvertDtoToEntity(DTO dto)
        {
            T t = Activator.CreateInstance<T>();
            if (dto == null)
            {
                return t;
            }
            Type entityType = dto.GetType();
            var toProps = t.GetType().GetProperties();
            foreach (var property in toProps)
            {
                PropertyInfo pi = entityType.GetProperty(property.Name);
                if (pi != null)
                {
                    if (pi.PropertyType == property.PropertyType)
                    {
                        property.SetValue(dto, pi.GetValue(dto, null));
                    }
                    else
                    {
                        throw new InvalidOperationException("转换类型失败");
                    }
                }
            }
            return t;
        }

        public virtual ResponseBase GetPageData(PageDataOptions pageData)
        {
            var list = repository.GetAll();
            var result = new List<DTO>();
            if (list!=null&& list.Count>0)
            {
                foreach (var item in list)
                {
                    result.Add(ConvertEntityToDto(item));
                }
            }
            return Result(result);
        }

    }
}
