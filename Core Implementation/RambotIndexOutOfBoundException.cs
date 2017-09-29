using System;
using System.Runtime.Serialization;

namespace Rambot.Core.Impl
{
    [Serializable]
    internal class RambotIndexOutOfBoundException : Exception
    {
        public RambotIndexOutOfBoundException()
        {
        }

        public RambotIndexOutOfBoundException(string message) : base(message)
        {
        }

        public RambotIndexOutOfBoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RambotIndexOutOfBoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}