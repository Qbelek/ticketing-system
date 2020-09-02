using System;
using MediatR;
using TMS.Shared;
using TMS.Shared.DTO;

namespace TMS.Tickets.Integration.Queries
{
    public class GetTicketQuery : IRequest<Response<TicketDTO>>
    {
        public GetTicketQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}