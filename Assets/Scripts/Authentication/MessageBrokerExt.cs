using System;
using UniRx;

namespace Authentication
{
    public static class MessageBrokerExt
    {
        public static void PublishAndThrow(this Exception message)
        {
            MessageBroker.Default.Publish(message);
            throw message;
        }
    }
}