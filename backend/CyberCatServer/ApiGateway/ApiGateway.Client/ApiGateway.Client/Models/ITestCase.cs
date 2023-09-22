namespace ApiGateway.Client.Models
{
    public interface ITestCase
    {
        object[] Inputs { get; }
        object Expected { get; }
    }
}