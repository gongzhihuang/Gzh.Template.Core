using Gzh.Template.Core.Repository.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gzh.Template.Core.Repository.DatabaseContext
{
    public class MongoDBContext
    {
        
        private readonly IMongoDatabase _mongoDBContext;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options">mongodb连接信息</param>
        public MongoDBContext(IOptions<DBSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _mongoDBContext = client.GetDatabase(options.Value.Database);
        }

        /// <summary>
        /// 获取表（集合）
        /// </summary>
        /// <typeparam name="TEntity">继承EntityMongoDB的实体类</typeparam>
        /// <param name="tableName"></param>
        /// <returns>IMongoCollection<TEntity></returns>
        public IMongoCollection<TEntity> GetCollection<TEntity>(string tableName = "") where TEntity : class
        {


            string dt = DateTime.Now.ToString("yyyy-MM-dd");
            // 如果表名（集合名）为空，以当前日期为表名
            if (!string.IsNullOrEmpty(tableName))
            {

                dt = tableName;
            }

            // 获取集合名称，使用的标准是在实体类型名后添加日期
            string collectionName = dt;

            // 如果集合不存在，那么创建集合
            if (false ==  IsCollectionExists<TEntity>(collectionName))
            {
                 _mongoDBContext.CreateCollection(collectionName);
            }


            return _mongoDBContext.GetCollection<TEntity>(collectionName);
        }

        /// <summary>
        /// 集合是否存在
        /// </summary>
        /// <typeparam name="TEntity">继承EntityMongoDB的实体类</typeparam>
        /// <returns></returns>
        public bool IsCollectionExists<TEntity>(string name)
        {
            var filter = new BsonDocument("name", name);
            // 通过集合名称过滤
            var collections =  _mongoDBContext.ListCollections(new ListCollectionsOptions { Filter = filter });
            // 检查是否存在
            return  collections.Any();
        } 
    }
}
