using System;
using TNRD;

namespace UI
{
    public interface ITextField
    {
        string Text { get; set; }
    }

    [Serializable]
    public class TextField : SerializableInterface<ITextField>, ITextField
    {
        public string Text
        {
            get => Value.Text;
            set => Value.Text = value;
        }
    }
}