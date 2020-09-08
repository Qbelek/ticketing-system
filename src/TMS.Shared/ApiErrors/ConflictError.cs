using System.Collections.Generic;
using System.Net;

namespace TMS.Shared.ApiErrors
{
    public class ConflictError : Error
    {
        public ConflictError(string message)
            : base(409, HttpStatusCode.Conflict.ToString(), message)
        { }
        
        public ConflictError(List<string> messages)
            : base(409, HttpStatusCode.Conflict.ToString(), messages)
        { }
    }
}