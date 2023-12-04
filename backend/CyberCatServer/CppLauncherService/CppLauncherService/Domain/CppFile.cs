namespace CppLauncherService.Domain;

public class CppFile
{
    public string FileName { get; }
    public string Content { get; }

    public CppFile(string fileName, string content)
    {
        fileName = Path.GetFileNameWithoutExtension(fileName);

        FileName = $"{fileName}.cpp";
        Content = content;
    }
}