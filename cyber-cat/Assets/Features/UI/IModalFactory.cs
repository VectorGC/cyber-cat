public interface IModalFactory
{
    IModal Ok(string header, string body, string okButton = "OK");
}