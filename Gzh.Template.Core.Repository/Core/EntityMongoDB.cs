using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gzh.Template.Core.Repository.Core
{
    /// <summary>
    /// 用于mongodb  实体类
    /// </summary>
    public abstract class EntityMongoDB
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
