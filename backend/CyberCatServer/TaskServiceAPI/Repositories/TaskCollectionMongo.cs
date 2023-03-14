using TaskServiceAPI.Models;
using MongoDB;
using MongoDB.Driver;
using System.Linq;

namespace TaskServiceAPI.Repositories
{
    public class TaskCollectionMongo : ITaskCollection
    {
        private readonly IMongoCollection<ProgTask> _taskCollection;
        public TaskCollectionMongo(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");
            var mongoClient = new MongoClient(connectionString);
            var cyberCatDB = mongoClient.GetDatabase("CyberCat");
            _taskCollection = cyberCatDB.GetCollection<ProgTask>("Tasks");
        }

        public async Task Add(ProgTask task)
        {
            await _taskCollection.InsertOneAsync(task);
        }

        public async Task<ProgTask> GetTask(int id)
        {
           var cursor = await _taskCollection.FindAsync(t => t.Id == id);
           return cursor.FirstOrDefault();
        }
    }
}
 