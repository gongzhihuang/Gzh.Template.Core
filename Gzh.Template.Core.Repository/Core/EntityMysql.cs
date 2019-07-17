using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gzh.Template.Core.Repository.Core
{
    /// <summary>
    /// 用于mysql 实体类
    /// </summary>
    public class EntityMysql
    {
        [Key]//主键
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//自增列
        [Column("id")]
        public int Id { get; set; }
    }
}
