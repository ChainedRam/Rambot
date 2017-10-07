using System;
using System.Runtime.Serialization;

namespace KLDev.Rambot.Exceptions
{
    [Serializable]
    internal class RambotUnknownServiceException : Exception
    {
        public RambotUnknownServiceException()
        {
        }

        public RambotUnknownServiceException(string message) : base(message)
        {
        }

        public RambotUnknownServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RambotUnknownServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}