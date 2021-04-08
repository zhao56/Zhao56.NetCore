using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhao56.Core.BaseModel
{
    public class PageDataOptions
    {
        public int Page { get; set; }
        public int Rows { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; }
       /// <summary>
       /// 排序方式
       /// </summary>
        public string Order { get; set; }
        //List<SearchParameters> searchParameters { get; set; }
    }

    public class SearchParameters
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string OpType { get; set; }
    }
}
