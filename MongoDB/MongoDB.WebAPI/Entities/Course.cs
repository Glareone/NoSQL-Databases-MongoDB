using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB.WebAPI.Entities;

public class Course
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("courseName")]
    public string? CourseName { get; set; }

    [BsonElement("courseType")]
    public int CourseType { get; set; }
}