using System;
using System.Runtime.Serialization;

namespace KLDev.Rambot.Exceptions
{
    [Serializable]
    internal class RambotRegexDuplicateMatchesException : Exception
    {
        public RambotRegexDuplicateMatchesException()
        {
        }

        public RambotRegexDuplicateMatchesException(string message) : base(message)
        {
        }

        public RambotRegexDuplicateMatchesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RambotRegexDuplicateMatchesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}