using System;
using Gzh.Template.Core.Repository.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gzh.Template.Core.Repository.Domain.MongoDBEntity
{
    /// <summary>
    /// 存储在MongoDB的User表
    /// </summary>
    public class User : EntityMongoDB
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [BsonElement("Name")]
        public string UserName { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [BsonElement("Birthday")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)] //mongodb中是以UTC时间存储
        public DateTime Birthday { get; set; }

    }
}
