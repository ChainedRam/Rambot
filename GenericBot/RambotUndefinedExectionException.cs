using System;
using System.Runtime.Serialization;

namespace Rambot.Core.Impl
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