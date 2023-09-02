using Shared.Server.Dto;

namespace TaskService.Repositories.InternalModels;

internal class TestDbModel
{
    public string Input { get; set; }
    public string ExpectedOutput { get; set; }

    public TestDbModel(TestDto test)
    {
        Input = test.Input;
        ExpectedOutput = test.ExpectedOutput;
    }

    public TestDbModel()
    {
    }

    public TestDto ToDto()
    {
        return new TestDto()
        {
            Input = Input,
            ExpectedOutput = ExpectedOutput
        };
    }
}