using System;
using System.Runtime.Serialization;

namespace Rambot.Core.Impl
{
    [Serializable]
    internal class RambotDuplicateService : Exception
    {
        public RambotDuplicateService()
        {
        }

        public RambotDuplicateService(string message) : base(message)
        {
        }

        public RambotDuplicateService(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RambotDuplicateService(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}