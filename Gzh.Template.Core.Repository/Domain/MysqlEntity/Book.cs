using Gzh.Template.Core.Repository.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gzh.Template.Core.Repository.Domain.MysqlEntity
{
    /// <summary>
    /// 存储在MySQL中的Book表
    /// </summary>
    public class Book : EntityMysql
    {
        /// <summary>
        /// book名称
        /// </summary>
        public string BookName { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }
    }
}
