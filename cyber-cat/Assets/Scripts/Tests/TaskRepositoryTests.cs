using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Models;
using NUnit.Framework;
using Repositories.TaskRepositories;
using RestAPI;

public class MockRestApi : IRestAPI
{
    public UniTask<ITokenSession> Authenticate(string login, string password, IProgress<float> progress = null)
    {
        throw new NotImplementedException();
    }

    Task<ITasks> IRestAPI.GetTasks(IProgress<float> progress)
    {
        throw new NotImplementedException();
    }

    public UniTask<string> GetTasks(IProgress<float> progress = null)
    {
        // TODO: Хз как сделать красивее.
        return UniTask.FromResult("{" +
                                    "\"tasks\" : " +
                                    "{" +
                                        "\"0\" : " +
                                        "{" +
                                            "\"name\": \"stub_task\"" +
                                        "}" +
                                    "}" +
                                  "}");
    }
}

[TestFixture]
public class TaskRepositoryTests
{
    private ITaskRepository _taskRepository;

    [SetUp]
    public void Setup()
    {
        /*
        _taskRepository = new TaskRepositoryRestProxy(new MockRestApi());
        */
    }

    [Test]
    public async Task HasTasks_WhenRequestTasks([Values("0")] string taskId)
    {
        var task = await _taskRepository.GetTask(taskId);
        
        Assert.AreEqual("stub_task", task.Name);
        Assert.AreEqual(string.Empty, task.Description);
    }
}