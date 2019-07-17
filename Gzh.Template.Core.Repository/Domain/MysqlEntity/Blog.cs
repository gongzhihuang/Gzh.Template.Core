using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Gzh.Template.Core.Repository.Core;

namespace Gzh.Template.Core.Repository.Domain.MysqlEntity
{
    public class Blog : EntityMysql
    {
        [Column("url")]
        public string Url { get; set; }

        //public ICollection<Post> Posts { get; set; }
    }
}
