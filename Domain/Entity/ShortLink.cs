using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Domain.Entity
{
    [BsonIgnoreExtraElements]
    public partial class ShortLinkModels
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public required string Title { get; set; }
        public required Uri ShortLink { get; set; }
        public required Uri Link { get; set; }
        public required bool Status { get; set; }
        public string[]? Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime Validity { get; set; }
    }
}
