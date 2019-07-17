using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gzh.Template.Core.Repository.Interface
{
    /// <summary>
    /// Mysql仓储接口
    /// 使用linq查询的仓储继承此接口
    /// 常用于对单个领域类操作数据，用lambda表达式查询比较方便，易于理解
    /// </summary>
    /// <typeparam name="T">存储于mysql的实体类</typeparam>
    public interface IMysqlRepository<T> where T : class
    {
        /// <summary>
        /// 查询一个实体
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        T FindSingle(Expression<Func<T, bool>> exp);

        /// <summary>
        /// 查询是否存在某个实体
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        bool IsExist(Expression<Func<T, bool>> exp);

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        IQueryable<T> Find(Expression<Func<T, bool>> exp);

        IQueryable<T> Find(Expression<Func<T, bool>> exp, int pageindex = 1, int pagesize = 10, string orderby = "");

        /// <summary>
        /// 查询符合条件的实体个数
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        int GetCount(Expression<Func<T, bool>> exp);

        /// <summary>
        /// 添加一个实体
        /// </summary>
        /// <returns>The add.</returns>
        /// <param name="entity">Entity.</param>
        bool Add(T entity);

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <returns><c>true</c>, if add was batched, <c>false</c> otherwise.</returns>
        /// <param name="entities">Entities.</param>
        bool BatchAdd(List<T> entities);

        /// <summary>
        /// 通过id更新一个实体的所有属性
        /// </summary>
        /// <returns>The update.</returns>
        /// <param name="entity">Entity.</param>
        bool Update(T entity);

        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="entity">Entity.</param>
        bool Delete(T entity);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <returns><c>true</c>, if delete was batched, <c>false</c> otherwise.</returns>
        /// <param name="entities">Entities.</param>
        bool BatchDelete(List<T> entities);


        /// <summary>
        /// 更新
        /// </summary>
        /// <returns>The update.</returns>
        /// <param name="exp">Exp.</param>
        /// <param name="entity">Entity.</param>
        //bool Update(Expression<Func<T, bool>> exp, T entity);

        /// <summary>
        /// 根据条件删除
        /// 有bug，暂不使用
        /// </summary>
        //void Delete(Expression<Func<T, bool>> exp);

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns><c>true</c>, if changes was saved, <c>false</c> otherwise.</returns>
        bool SaveChanges();

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <returns>影响行数</returns>
        /// <param name="sql">Sql.</param>
        int ExecuteSql(string sql);
    }
}
