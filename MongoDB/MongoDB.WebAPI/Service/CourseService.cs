using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.WebAPI.Entities;
using MongoDB.WebAPI.MongoDBConfigurations;

namespace MongoDB.WebAPI.Service;

public class CourseService
{
    private readonly IMongoCollection<Course> _courseCollection;

    public CourseService(IOptions<CoursesConfig> courseConfigOptions, IOptions<MongoDBConfig> mongoDbOptions) {
        MongoClient client = new(mongoDbOptions.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(courseConfigOptions.Value.DatabaseName);
        _courseCollection = database.GetCollection<Course>(courseConfigOptions.Value.CollectionName);
    }

    public async Task<IReadOnlyCollection<Course>> GetAsync()
    {
        return await _courseCollection.Find(Builders<Course>.Filter.Empty).ToListAsync();
    }

    public async Task CreateAsync(Course course)
    {
       await _courseCollection.InsertOneAsync(course);
    }

    public async Task DeleteAsync(string id)
    {
        await _courseCollection.DeleteOneAsync(id);
    }

    public async Task<bool> UpdateAsync(string oldCourseName, string newCourseName)
    {
        var filter = Builders<Course>.Filter
            .Eq(restaurant => restaurant.CourseName, oldCourseName);
        var update = Builders<Course>.Update
            .Set(restaurant => restaurant.CourseName, newCourseName);
        
        var result = await _courseCollection.UpdateOneAsync(filter, update);

        return result.ModifiedCount > 0;
    }
}