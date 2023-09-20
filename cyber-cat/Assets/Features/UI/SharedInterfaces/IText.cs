using System.Threading.Tasks;

public interface IText
{
    string Text { get; set; }
    void SetTextAsync(Task<string> handler);
}