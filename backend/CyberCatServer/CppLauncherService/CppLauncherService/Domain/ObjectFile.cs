namespace CppLauncherService.Domain;

public class ObjectFile
{
    public string FileName { get; }

    public ObjectFile(CppFile cppFile)
    {
        var fileName = Path.GetFileNameWithoutExtension(cppFile.FileName);
        FileName = $"{fileName}.o";
    }
}