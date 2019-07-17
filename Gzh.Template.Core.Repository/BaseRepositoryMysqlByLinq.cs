using Gzh.Template.Core.Repository.DatabaseContext;
using Gzh.Template.Core.Repository.Domain.MysqlEntity;
using Gzh.Template.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gzh.Template.Core.Repository
{
    /// <summary>
    /// 使用linq查询的Mysql的基础仓储实现类
    /// </summary>
    public class BaseRepositoryMysqlByLinq : IMysqlRepositoryByLinq
    {
        protected readonly MysqlContext _context;

        public BaseRepositoryMysqlByLinq(MysqlContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 获取MysqlContext的实例
        /// </summary>
        /// <returns>The context.</returns>
        public MysqlContext GetContext()
        {
            return _context;
        }
    }
}
