using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gzh.Template.Core.IdentityServer.Models
{
    /// <summary>
    /// 自定义用户
    /// </summary>
    public class User
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("age")]
        public int Age { get; set; }
    }
}
