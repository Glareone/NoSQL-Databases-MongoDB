using MongoDB.Driver;

var connectionString = "mongodb://localhost:27017";
var client = new MongoClient(connectionString);
var dblist = await client.ListDatabases().ToListAsync();

foreach (var dbInstance in dblist)
{
    Console.WriteLine($"db instance: {dbInstance}");
}