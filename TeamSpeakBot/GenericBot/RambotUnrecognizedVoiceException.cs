using System;
using System.Runtime.Serialization;

namespace Rambot.Core.Impl
{
    [Serializable]
    internal class RambotDuplicateVoiceMatchException : Exception
    {
        public RambotDuplicateVoiceMatchException()
        {
        }

        public RambotDuplicateVoiceMatchException(string message) : base(message)
        {
        }

        public RambotDuplicateVoiceMatchException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RambotDuplicateVoiceMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}