using System;
using MediatR;
using TMS.Shared;

namespace TMS.Tickets.Integration.Queries
{
    public class GetTicketQuery : IRequest<Response>
    {
        public GetTicketQuery(Guid ticketId)
        {
            TicketId = ticketId;
        }

        public Guid TicketId { get; }
    }
}