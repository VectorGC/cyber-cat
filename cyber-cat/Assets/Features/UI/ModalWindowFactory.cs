public class ModalWindowFactory : IModalFactory
{
    public IModal Ok(string header, string body, string okButton = "OK")
    {
        var window = SimpleModalWindow.Create()
            .SetHeader(header)
            .SetBody(body)
            .AddButton(okButton);

        return new SimpleModalWindowAdapter(window);
    }
}