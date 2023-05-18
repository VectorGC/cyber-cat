namespace CompilerServiceAPI.Services;

internal class TempCppFileCreator : ICppFileCreator
{
    public async Task<string> CreateCppWithText(string sourceCode)
    {
        var fileName = Path.GetRandomFileName();
        fileName = Path.GetFileNameWithoutExtension(fileName);
        var fileNameCpp = $"{fileName}.cpp";

        await using var writer = File.CreateText(fileNameCpp);
        await writer.WriteAsync(sourceCode);

        return fileNameCpp;
    }

    public string GetObjectFileName(string cppFileName)
    {
        var fileName = Path.GetFileNameWithoutExtension(cppFileName);
        return $"{fileName}.o";
    }
}