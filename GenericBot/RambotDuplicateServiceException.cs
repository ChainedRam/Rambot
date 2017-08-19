using System;
using System.Runtime.Serialization;

namespace Rambot.Core.Impl
{
    [Serializable]
    internal class RambotDuplicateServiceException : Exception
    {
        public RambotDuplicateServiceException()
        {
        }

        public RambotDuplicateServiceException(string message) : base(message)
        {
        }

        public RambotDuplicateServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RambotDuplicateServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}