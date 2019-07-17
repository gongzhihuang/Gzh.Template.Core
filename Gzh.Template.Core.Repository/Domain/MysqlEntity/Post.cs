using System;
using System.ComponentModel.DataAnnotations.Schema;
using Gzh.Template.Core.Repository.Core;

namespace Gzh.Template.Core.Repository.Domain.MysqlEntity
{
    public class Post : EntityMysql
    {
        [Column("title")]
        public string Title { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [Column("blog_id")]
        public int BlogId { get; set; }

        //public Blog Blog { get; set; }
    }
}
