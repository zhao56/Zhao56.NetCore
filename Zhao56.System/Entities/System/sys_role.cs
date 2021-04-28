using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zhao56.Core.BaseModel;

namespace Zhao56.System.Entities
{
    [Table("Sys_Role")]
    public class sys_role : BaseEntity
    {
        /// <summary>
        ///角色ID
        /// </summary>
        [Key]
       public string Role_Id { get; set; }

       /// <summary>
        ///父级ID
        /// </summary>
       public string ParentId { get; set; }

       /// <summary>
        ///角色名称
        /// </summary>
       public string RoleName { get; set; }

       /// <summary>
        ///部门ID
        /// </summary>
       public string Dept_Id { get; set; }

       /// <summary>
        ///部门名称
        /// </summary>
       public string DeptName { get; set; }

       /// <summary>
        ///排序
        /// </summary>
       public string OrderNo { get; set; }

       /// <summary>
        ///创建人
        /// </summary>
       public string Creator { get; set; }

       /// <summary>
        ///创建时间
        /// </summary>
       public string CreateDate { get; set; }

       /// <summary>
        ///修改人
        /// </summary>
       public string Modifier { get; set; }

       /// <summary>
        ///修改时间
        /// </summary>
       public string ModifyDate { get; set; }

       /// <summary>
        ///
        /// </summary>
       public string DeleteBy { get; set; }

       /// <summary>
        ///是否启用
        /// </summary>
       public string Enable { get; set; }

       
    }
}
