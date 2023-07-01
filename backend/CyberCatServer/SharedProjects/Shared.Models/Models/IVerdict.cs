namespace Shared.Models.Models
{
    public interface IVerdict
    {
        VerdictStatus Status { get; }
        string Error { get; }
        int TestsPassed { get; }
    }
}