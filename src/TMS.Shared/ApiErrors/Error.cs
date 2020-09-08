using System.Collections.Generic;

namespace TMS.Shared.ApiErrors
{
    public abstract class Error
    {
        protected Error(int statusCode, string statusDescription, string message)
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
            Messages = new List<string>();
            Messages.Add(message);
        }

        protected Error(int statusCode, string statusDescription, List<string> messages)
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
            Messages = messages;
        }

        public int StatusCode { get; }

        public string StatusDescription { get; }

        public List<string> Messages { get; }
    }
}