using System;
using System.Runtime.Serialization;

namespace KLDev.Rambot.Exceptions
{
    [Serializable]
    internal class RambotUndefinedVoiceException : Exception
    {
        public RambotUndefinedVoiceException()
        {
        }

        public RambotUndefinedVoiceException(string message) : base(message)
        {
        }

        public RambotUndefinedVoiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RambotUndefinedVoiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}