using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zhao56.Core.BaseModel;

namespace Zhao56.System.Entities
{
    [Table("sys_dictionary")]
    public class sys_dictionary : BaseEntity
    {
        /// <summary>
        ///字典ID
        /// </summary>
        [Key]
       public string Dic_ID { get; set; }

       /// <summary>
        ///字典名称
        /// </summary>
       public string DicName { get; set; }

       /// <summary>
        ///父级ID
        /// </summary>
       public string ParentId { get; set; }

       /// <summary>
        ///配置项
        /// </summary>
       public string Config { get; set; }

       /// <summary>
        ///sql语句
        /// </summary>
       public string DbSql { get; set; }

       /// <summary>
        ///DBServer
        /// </summary>
       public string DBServer { get; set; }

       /// <summary>
        ///排序号
        /// </summary>
       public string OrderNo { get; set; }

       /// <summary>
        ///字典编号
        /// </summary>
       public string DicNo { get; set; }

       /// <summary>
        ///备注
        /// </summary>
       public string Remark { get; set; }

       /// <summary>
        ///是否启用
        /// </summary>
       public string Enable { get; set; }

       /// <summary>
        ///
        /// </summary>
       public string CreateID { get; set; }

       /// <summary>
        ///创建人
        /// </summary>
       public string Creator { get; set; }

       /// <summary>
        ///创建时间
        /// </summary>
       public string CreateDate { get; set; }

       /// <summary>
        ///
        /// </summary>
       public string ModifyID { get; set; }

       /// <summary>
        ///修改人
        /// </summary>
       public string Modifier { get; set; }

       /// <summary>
        ///修改时间
        /// </summary>
       public string ModifyDate { get; set; }

       
    }
}
