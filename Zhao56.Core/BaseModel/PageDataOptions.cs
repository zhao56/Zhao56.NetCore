using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.BaseModel.Paging;

namespace Zhao56.Core.BaseModel
{
    public class PageDataOptions
    {
        public Pager Pager { get; set; }
        public List<OrderParameters> Orders { get; set; }
        public List<SearchParameters> Wheres { get; set; }
    }

    public class SearchParameters
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string OpType { get; set; }
    }
    public class OrderParameters
    {
        public string Name { get; set; }
        public bool IsAsc { get; set; }
    }
}
