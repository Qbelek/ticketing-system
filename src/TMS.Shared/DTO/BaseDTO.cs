using System;
using System.Text.Json.Serialization;

namespace TMS.Shared.DTO
{
    [JsonConverter(typeof(RuntimeTypeConverter<BaseDTO>))]
    public abstract class BaseDTO
    {
        public Guid? Id { get; set; }
    }
}