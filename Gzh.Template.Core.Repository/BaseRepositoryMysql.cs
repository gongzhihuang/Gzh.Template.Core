using Gzh.Template.Core.Repository.Core;
using Gzh.Template.Core.Repository.DatabaseContext;
using Gzh.Template.Core.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gzh.Template.Core.Repository
{
    /// <summary>
    /// 使用lambda表达式查询的Mysql的基础仓储实现类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepositoryMysql<T> : IMysqlRepository<T> where T : EntityMysql
    {
        protected readonly MysqlContext _context;

        public BaseRepositoryMysql(MysqlContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 添加一个实体
        /// </summary>
        /// <returns>The add.</returns>
        /// <param name="entity">Entity.</param>
        public bool Add(T entity)
        {
            _context.Add(entity);
            //_context.SaveChanges();
            return SaveChanges();
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <returns><c>true</c>, if add was batched, <c>false</c> otherwise.</returns>
        /// <param name="entities">Entities.</param>
        public bool BatchAdd(List<T> entities)
        {
            _context.AddRange(entities);
            return SaveChanges();
        }

        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="entity">Entity.</param>
        public bool Delete(T entity)
        {
            _context.Remove(entity);
            //_context.SaveChanges();
            return SaveChanges();
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <returns><c>true</c>, if delete was batched, <c>false</c> otherwise.</returns>
        /// <param name="entity">Entity.</param>
        public bool BatchDelete(List<T> entity)
        {
            //_context.Remove(entity);
            //foreach (var item in entity)
            //{
            //    _context.Remove(item);
            //}
            _context.RemoveRange(entity);
            //_context.SaveChanges();
            return SaveChanges();
        }

        /// <summary>
        /// 根据条件删除
        /// 有bug，暂不使用
        /// </summary>
        //public void Delete(Expression<Func<T, bool>> exp)
        //{
        //    _context.Remove(exp);
        //    _context.SaveChanges();
        //}

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <returns>影响行数</returns>
        /// <param name="sql">Sql.</param>
        public int ExecuteSql(string sql)
        {
            return _context.Database.ExecuteSqlCommand(sql);
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public IQueryable<T> Find(Expression<Func<T, bool>> exp)
        {
            return _context.Set<T>().Where(exp);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> exp, int pageindex = 1, int pagesize = 10, string orderby = "")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询一个实体
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public T FindSingle(Expression<Func<T, bool>> exp)
        {
            T t = _context.Set<T>().Where(exp).FirstOrDefault();
            return t;
        }

        /// <summary>
        /// 查询符合条件的实体个数
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public int GetCount(Expression<Func<T, bool>> exp)
        {
            return _context.Set<T>().Where(exp).Count();
        }

        /// <summary>
        /// 查询是否存在某个实体
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public bool IsExist(Expression<Func<T, bool>> exp)
        {
            return _context.Set<T>().Any(exp);
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns><c>true</c>, if changes was saved, <c>false</c> otherwise.</returns>
        public bool SaveChanges()
        {
            int state = _context.SaveChanges();
            if (state > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 通过id更新一个实体的所有属性
        /// </summary>
        /// <returns>The update.</returns>
        /// <param name="entity">Entity.</param>
        public bool Update(T entity)
        {
            //T res = _context.Set<T>().Where(x => x.Id == entity.Id).First();
            //res = entity;
            _context.Update(entity);
            return SaveChanges();
            //_context.SaveChanges();
            //var entry = this._context.Entry(entity);
            //entry.State = EntityState.Modified;

            ////如果数据没有发生变化
            //if (!_context.ChangeTracker.HasChanges())
            //{
            //    return;
            //}
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns>The update.</returns>
        /// <param name="exp">更新条件</param>
        /// <param name="entity">更新后的实体</param>
        //public bool Update(Expression<Func<T, bool>> exp, T entity)
        //{
        //    T res = _context.Set<T>().Where(exp).First();
        //    res = entity;
        //    return SaveChanges();
        //}
    }
}
