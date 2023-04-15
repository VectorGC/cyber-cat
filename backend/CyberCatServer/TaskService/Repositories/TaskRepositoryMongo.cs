using TaskService.Models;
using MongoDB;
using MongoDB.Driver;
using System.Linq;

namespace TaskService.Repositories
{
    public class TaskRepositoryMongo : ITaskRepository
    {
        private readonly IMongoCollection<ProgTaskDbModel> _taskRepository;
        public TaskRepositoryMongo(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");
            Console.WriteLine(connectionString);
            var mongoClient = new MongoClient(connectionString);
            
            var cyberCatDB = mongoClient.GetDatabase("CyberCat");
            _taskRepository = cyberCatDB.GetCollection<ProgTaskDbModel>("Tasks");
        }

        public async Task Add(ProgTaskDbModel task)
        {
            await _taskRepository.InsertOneAsync(task);
        }

        public async Task<ProgTaskDbModel> GetTask(int id)
        {
           var cursor = await _taskRepository.FindAsync(t => t.Id == id);
           return cursor.FirstOrDefault();
        }
    }
}
 