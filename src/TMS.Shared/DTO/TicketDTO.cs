using System;

namespace TMS.Shared.DTO
{
    public class TicketDTO : BaseDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public UserDTO Issuer { get; set; }
        public UserDTO Assignee { get; set; }
        public DateTimeOffset? CreationDate { get; set; }
        public DateTimeOffset? LastModificationDate { get; set; }
    }
}