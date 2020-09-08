using System;
using System.Text.Json.Serialization;
using TMS.Shared.ApiErrors;
using TMS.Shared.DTO;

namespace TMS.Shared
{
    public class Response
    {
        public Response(BaseDTO data, MachineDateTime mdt)
        {
            Data = data;
            IsValid = true;
            Timestamp = mdt.Now();
        }

        public Response(Error error, MachineDateTime mdt)
        {
            Error = error;
            Timestamp = mdt.Now();
        }

        public BaseDTO Data { get; }
        public Error Error { get; }

        [JsonIgnore] public bool IsValid { get; }

        public DateTimeOffset Timestamp { get; }
    }
}