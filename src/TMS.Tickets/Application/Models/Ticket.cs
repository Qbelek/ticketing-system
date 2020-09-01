using System;

namespace TMS.Tickets.Application.Models
{
    internal class Ticket
    {
        public string Title { get; }
        public string Description { get; }
        public User Issuer { get; }
        public User Assignee { get; }
        public TicketState State { get; }
        public DateTimeOffset CreationDate { get; }
        public DateTimeOffset LastModificationDate { get; }
    }
}