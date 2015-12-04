using System;

namespace StudentHome.Api.Service
{
    public class RepositoryException : Exception
    {
        private string error;
        public RepositoryException(string error)
        {
            this.error = error;
        }

        public override string Message
        {
            get { return error; }
        }
    }
}
