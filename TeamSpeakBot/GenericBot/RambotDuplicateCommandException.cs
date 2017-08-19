using System;
using System.Runtime.Serialization;

namespace Rambot.Core.Impl
{
    [Serializable]
    internal class RambotDuplicateCommandException : Exception
    {
        public RambotDuplicateCommandException()
        {
        }

        public RambotDuplicateCommandException(string message) : base(message)
        {
        }

        public RambotDuplicateCommandException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RambotDuplicateCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}