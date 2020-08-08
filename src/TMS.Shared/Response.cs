using TMS.Shared.ApiErrors;
using TMS.Shared.DTO;

namespace TMS.Shared
{
    public class Response<T> where T : BaseDTO
    {
        public Response(T value)
        {
            Value = value;
            IsValid = true;
        }

        public Response(Error error)
        {
            Error = error;
        }
        
        public T Value { get; }
        public Error Error { get; }
        public bool IsValid { get; }
    }
}