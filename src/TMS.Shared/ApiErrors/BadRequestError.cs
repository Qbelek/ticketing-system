using System.Net;

namespace TMS.Shared.ApiErrors
{
    public class BadRequestError : Error
    {
        public BadRequestError(string message)
            : base(400, HttpStatusCode.BadRequest.ToString(), message)
        {
        }
    }
}