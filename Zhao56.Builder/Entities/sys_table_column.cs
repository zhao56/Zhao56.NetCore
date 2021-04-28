using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zhao56.Core.BaseModel;

namespace Zhao56.Builder.Entities
{
    [Table("Sys_TableColumn")]
    public class sys_table_column : BaseEntity
    {
        [Key]
        public int Table_Id { get; set; }

        
        public int ColumnId { get; set; }

        
        public string ColumnName { get; set; }

        
        public string ColumnCnName { get; set; }

        
        public string ColumnType { get; set; }

        
        public string TableName { get; set; }

        
        public int? Maxlength { get; set; }

       
        public int? IsNull { get; set; }

        
        public int? IsDisplay { get; set; }

        
        public int? IsKey { get; set; }

        
        public string Columnformat { get; set; }

        
        public string Script { get; set; }

        
        public string DropNo { get; set; }

        
        public int? IsImage { get; set; }
        
        public int? Sortable { get; set; }

        
        public int? ColumnWidth { get; set; }

        
        public int? SearchRowNo { get; set; }
        
        public int? SearchColNo { get; set; }
        
        public string SearchType { get; set; }

        
        public int? EditRowNo { get; set; }

        
        public int? EditColNo { get; set; }
        
        public string EditType { get; set; }
        
        public int? ColSize { get; set; }

        
        public int? IsReadDataset { get; set; }
        
        public int? Enable { get; set; }

        
        public int? ApiInPut { get; set; }
        
        public int? ApiIsNull { get; set; }
        
        public int? ApiOutPut { get; set; }
        
        public int? CreateID { get; set; }

        
        public string Creator { get; set; }

        
        public DateTime? CreateDate { get; set; }

        
        public int? ModifyID { get; set; }

        
        public string Modifier { get; set; }

        
        public DateTime? ModifyDate { get; set; }

        
        public int? OrderNo { get; set; }

        
        public int? IsColumnData { get; set; }
    }
}
