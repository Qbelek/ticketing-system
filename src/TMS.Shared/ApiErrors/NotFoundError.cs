using System.Collections.Generic;
using System.Net;

namespace TMS.Shared.ApiErrors
{
    public class NotFoundError : Error
    {
        public NotFoundError(string message)
            : base(404, HttpStatusCode.NotFound.ToString(), message)
        { }
        
        public NotFoundError(List<string> messages)
            : base(404, HttpStatusCode.NotFound.ToString(), messages)
        { }
    }
}