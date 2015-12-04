using System;
using System.Runtime.Serialization;

namespace StudentHome.Api.Service
{
    [Serializable]
    public class ServiceException : Exception
    {
        public ServiceException()
        {
        }

        public ServiceException(string message)
            : base(message)
        {
        }

        public ServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
