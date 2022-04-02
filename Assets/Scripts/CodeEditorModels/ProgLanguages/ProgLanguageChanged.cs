using UniRx;

namespace CodeEditorModels.ProgLanguages
{
    public struct ProgLanguageChanged
    {
        public ProgLanguage Language { get; }
        public string Text { get; }

        public ProgLanguageChanged(ProgLanguage language, string text)
        {
            Language = language;
            Text = text;
        }

        public void Publish()
        {
            MessageBroker.Default.Publish(this);
        }
    }
}