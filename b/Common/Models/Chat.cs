using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace login.Common.Models
{
    public class Chat
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("SenderId")]
        public string? SenderId { get; set; }

        [BsonElement("ReceiverId")]
        public string? ReceiverId { get; set; }

        [BsonElement("Message")]
        public string? Message { get; set; } // Message content or file URL

        [BsonElement("FileName")]
        public string? FileName { get; set; } // File name

        [BsonElement("FileType")]
        public string? FileType { get; set; } // File type

        [BsonElement("FileContent")]
        public byte[]? FileContent { get; set; }

        [BsonElement("FileSize")]
        public long? FileSize { get; set; } // File size

        [BsonRepresentation(BsonType.Int64)]
        public long? Timestamp { get; set; }
    }
}
