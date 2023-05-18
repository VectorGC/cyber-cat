namespace CompilerServiceAPI.InternalModels;

internal class CompileCppResult
{
    public string ObjectFileName { get; set; }
    public Output Output { get; set; }
}