using System;

namespace StudentHome.Api.Service
{
    public class ValidationException : Exception
    {
        private string errorMessage;

        public override string Message
        {
            get { return errorMessage; }
        }

        public ValidationException(string errorMessage)
        {
            this.errorMessage = errorMessage;
        }
    }
}
