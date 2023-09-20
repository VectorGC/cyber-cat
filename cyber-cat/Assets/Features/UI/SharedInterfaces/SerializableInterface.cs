using System;

[Serializable]
public class SerializableInterface<TInterface> : TNRD.SerializableInterface<TInterface> where TInterface : class
{
    public new TInterface Value => base.Value;
}