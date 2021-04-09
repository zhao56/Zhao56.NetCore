using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhao56.Core.Extensions
{
    public static class JsonCoverExtensions
    {
        public static string ObjectToJson(this Object obj, JsonSerializerSettings formatDate = null)
        {
            if (obj == null) return null;
            formatDate = formatDate ?? new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            };
            return JsonConvert.SerializeObject(obj, formatDate);
        }
    }
}
