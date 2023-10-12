using System.Threading.Tasks;
using UnityEngine;

public interface IText
{
    string Text { get; set; }
    Color Color { get; set; }
    void SetTextAsync(Task<string> handler);
}