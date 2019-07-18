using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gzh.Template.Core.Repository.Interface
{
    /// <summary>
    /// MongoDB仓储接口
    /// 使用lambda表达式查询的仓储继承此接口
    /// 常用于对单个领域类操作数据，用lambda表达式查询比较方便，易于理解
    /// </summary>
    /// <typeparam name="T">存储于mongodb的实体类</typeparam>
    public interface IMongoDBRepository<T> where T : class
    {
        /// <summary>
        /// 查询一个实体
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="tableName">表名(集合名)</param>
        /// <returns></returns>
        T FindSingle(Expression<Func<T, bool>> exp, string tableName = "");

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="tableName">表名(集合名)</param>
        /// <returns></returns>
        IQueryable<T> Find(Expression<Func<T, bool>> exp, string tableName = "");

        ///// <summary>
        ///// 分页查询
        ///// </summary>
        ///// <returns>The find.</returns>
        ///// <param name="pageindex">Pageindex.</param>
        ///// <param name="pagesize">Pagesize.</param>
        ///// <param name="orderby">Orderby.</param>
        ///// <param name="exp">Exp.</param>
        ///// <param name="tableName">Table name.</param>
        //IQueryable<T> Find(int pageindex = 1, int pagesize = 10, string orderby = "", Expression<Func<T, bool>> exp = null, string tableName = "");

        /// <summary>
        /// 查询实体个数
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="tableName">表名(集合名)</param>
        /// <returns></returns>
        int GetCount(Expression<Func<T, bool>> exp = null, string tableName = "");

        /// <summary>
        /// 添加一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableName">表名(集合名)</param>
        void Add(T entity, string tableName = "");

        /// <summary>
        /// 添加多个实体
        /// </summary>
        /// <param name="entities">Entities.</param>
        /// <param name="tableName">Table name.</param>
        void Adds(List<T> entities, string tableName = "");

        /// <summary>
        /// 更新一个实体的所有属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableName">表名(集合名)</param>
        void Update(T entity, string tableName = "");

        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableName">表名(集合名)</param>
        void Delete(T entity, string tableName = "");


        ///// <summary>
        ///// 实现按需要只更新部分更新
        ///// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        ///// </summary>
        ///// <param name="where">更新条件</param>
        ///// <param name="entity">更新后的实体</param>
        ///// <param name="tableName">表名(集合名)</param>
        //void Update(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity, string tableName = "");

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="tableName">表名(集合名)</param>
        void Delete(Expression<Func<T, bool>> exp, string tableName = "");
    }
}
