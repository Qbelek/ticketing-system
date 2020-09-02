using System.Net;

namespace TMS.Shared.ApiErrors
{
    public class ConflictError : Error
    {
        public ConflictError(string message)
            : base(409, HttpStatusCode.Conflict.ToString(), message)
        {
        }
    }
}