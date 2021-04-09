using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zhao56.Core.BaseModel
{
    public class BaseEntity: IBaseEntity
    {
        /// <summary>
        /// 通过反射转换对应的DTO
        /// </summary>
        /// <typeparam name="DTO"></typeparam>
        /// <returns></returns>
        public virtual DTO ConvertEntityToDto<DTO>() where DTO : DTOBase, new()
        {
            DTO dto = new DTO();
            if (this == null)
            {
                return default(DTO);
            }
            Type entityType = this.GetType();
            var toProps = dto.GetType().GetProperties();
            foreach (var property in toProps)
            {
                PropertyInfo pi = entityType.GetProperty(property.Name);
                if (pi != null)
                {
                    if (pi.PropertyType == property.PropertyType)
                    {
                        property.SetValue(dto, pi.GetValue(this, null));
                    }
                    else
                    {
                        throw new InvalidOperationException("转换类型失败");
                    }
                }
            }
            return dto;
        }
    }
}
