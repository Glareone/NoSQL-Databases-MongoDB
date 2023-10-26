using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.WebAPI.Entities;
using MongoDB.WebAPI.MongoDBConfigurations;

namespace MongoDB.WebAPI.Service;

public class CourseService
{
    private readonly IMongoCollection<Course> _courseCollection;

    public CourseService(IOptions<CoursesConfig> courseConfigOptions, IOptions<MongoDBConfig> mongoDbOptions) {
        MongoClient client = new MongoClient(mongoDbOptions.Value.ConnectionString);
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
}