using System;
using System.Collections.Generic;
using System.Text;

namespace Gzh.Template.Core.Repository
{
    /// <summary>
    /// mongodb数据库设置
    /// </summary>
    public class DBSettings
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string Database { get; set; }
    }
}
