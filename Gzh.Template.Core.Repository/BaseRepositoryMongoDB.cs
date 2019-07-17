using Gzh.Template.Core.Repository.Core;
using Gzh.Template.Core.Repository.DatabaseContext;
using Gzh.Template.Core.Repository.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gzh.Template.Core.Repository
{
    /// <summary>
    /// 使用lambda表达式查询的MongoDB的基础仓储实现类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepositoryMongoDB<T> : IMongoDBRepository<T> where T : EntityMongoDB
    {
        protected readonly MongoDBContext _context;
        public BaseRepositoryMongoDB(MongoDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 添加一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableName">表名(集合名)</param>
        public void Add(T entity, string tableName = "")
        {
            var collection = _context.GetCollection<T>(tableName);
            collection.InsertOne(entity);
        }

        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableName">表名(集合名)</param>
        public void Delete(T entity, string tableName = "")
        {
            var collection = _context.GetCollection<T>(tableName);
            collection.DeleteOne(x => x.Id == entity.Id);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="tableName">表名(集合名)</param>
        public void Delete(Expression<Func<T, bool>> exp, string tableName = "")
        {
            var collection = _context.GetCollection<T>(tableName);
            collection.DeleteOne(exp);
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="tableName">表名(集合名)</param>
        /// <returns></returns>
        public IQueryable<T> Find(Expression<Func<T, bool>> exp, string tableName = "")
        {
            var collection = _context.GetCollection<T>(tableName);
            return collection.AsQueryable<T>().Where(exp);
        }

        ///// <summary>
        ///// 分页查询
        ///// </summary>
        ///// <returns>The find.</returns>
        ///// <param name="pageindex">Pageindex.</param>
        ///// <param name="pagesize">Pagesize.</param>
        ///// <param name="orderby">Orderby.</param>
        ///// <param name="exp">Exp.</param>
        ///// <param name="tableName">Table name.</param>
        //public IQueryable<T> Find(int pageindex = 1, int pagesize = 10, string orderby = "", Expression<Func<T, bool>> exp = null, string tableName = "")
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// 查询符合条件的第一个实体
        /// </summary>
        /// <returns>The single.</returns>
        /// <param name="exp">Exp.</param>
        /// <param name="tableName">Table name.</param>
        public T FindSingle(Expression<Func<T, bool>> exp, string tableName = "")
        {
            var collection = _context.GetCollection<T>(tableName);
            return collection.AsQueryable<T>().FirstOrDefault(exp);
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <returns>The count.</returns>
        /// <param name="exp">Exp.</param>
        /// <param name="tableName">Table name.</param>
        public int GetCount(Expression<Func<T, bool>> exp, string tableName = "")
        {
            var collection = _context.GetCollection<T>(tableName);
            int count = (int)collection.CountDocuments(exp);
            return count;
        }

        /// <summary>
        /// 更新一个实体的所有属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableName">表名(集合名)</param>
        public void Update(T entity, string tableName = "")
        {
            var collection = _context.GetCollection<T>(tableName);
            collection.ReplaceOne(x => x.Id == entity.Id, entity);
        }
    }
}
