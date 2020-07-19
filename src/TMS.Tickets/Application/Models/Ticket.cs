using System;

namespace TMS.Tickets.Application.Models
{
    internal class Ticket
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public User Issuer { get; set; }
        public User Assignee { get; set; }
        public TicketState State { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset LastModificationDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset DeletionDate { get; set; }
    }
}