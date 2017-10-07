using System;
using System.Runtime.Serialization;

namespace KLDev.Rambot.Exceptions
{
    [Serializable]
    internal class RambotSpeechInteruptedException : Exception
    {
        public RambotSpeechInteruptedException()
        {
        }

        public RambotSpeechInteruptedException(string message) : base(message)
        {
        }

        public RambotSpeechInteruptedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RambotSpeechInteruptedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}