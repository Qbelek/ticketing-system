namespace TMS.Shared.ApiErrors
{
    public abstract class Error
    {
        protected Error(int statusCode, string statusDescription, string message = "")
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
            Message = message;
        }

        public int StatusCode { get; }

        public string StatusDescription { get; }

        public string Message { get; }
    }
}