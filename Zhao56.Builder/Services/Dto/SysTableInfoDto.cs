using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.BaseModel;

namespace Zhao56.Builder.Services.Dto
{
    public class SysTableInfoDto: DTOBase
    {
        public int Table_Id { get; set; }
        public int? ParentId { get; set; }

        public string TableName { get; set; }
        public string TableTrueName { get; set; }
        public string ColumnCNName { get; set; }


        public string Namespace { get; set; }

        public string FolderName { get; set; }

        public string DataTableType { get; set; }

        public string EditorType { get; set; }
        public int? OrderNo { get; set; }
        public string UploadField { get; set; }
        public int? UploadMaxCount { get; set; }
        public string RichText { get; set; }

        public string ExpressField { get; set; }

        public string DBServer { get; set; }

        public string SortName { get; set; }

        public string DetailCnName { get; set; }



        public string DetailName { get; set; }


        public int? Enable { get; set; }



        public string CnName { get; set; }
    }
}
