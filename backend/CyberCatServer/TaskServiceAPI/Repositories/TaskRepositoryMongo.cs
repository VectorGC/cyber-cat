using TaskServiceAPI.Models;
using MongoDB;
using MongoDB.Driver;
using System.Linq;

namespace TaskServiceAPI.Repositories
{
    public class TaskRepositoryMongo : ITaskRepository
    {
        private readonly IMongoCollection<ProgTaskDbModel> _taskCollection;
        public TaskRepositoryMongo(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");
            Console.WriteLine(connectionString);
            var mongoClient = new MongoClient(connectionString);
            
            var cyberCatDB = mongoClient.GetDatabase("CyberCat");
            _taskCollection = cyberCatDB.GetCollection<ProgTaskDbModel>("Tasks");
        }

        public async Task Add(ProgTaskDbModel task)
        {
            await _taskCollection.InsertOneAsync(task);
        }

        public async Task<ProgTaskDbModel> GetTask(int id)
        {
           var cursor = await _taskCollection.FindAsync(t => t.Id == id);
           return cursor.FirstOrDefault();
        }
    }
}
 