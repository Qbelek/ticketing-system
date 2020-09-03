using System;

namespace TMS.Tickets.Persistence.Rows
{
    public class TicketRow : BaseRow
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public UserRow Issuer { get; set; }
        public UserRow Assignee { get; set; }
        public string State { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset LastModificationDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletionDate { get; set; }
    }
}