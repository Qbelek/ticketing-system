using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Http;
using TMS.Shared;
using TMS.Shared.DTO;

namespace TMS.Tickets.Integration.Commands
{
    public class CreateTicketCommand : IRequest<Response>
    {
        public CreateTicketCommand(TicketDTO ticketDto, IList<IFormFile> attachments)
        {
            TicketDto = ticketDto;
            Attachments = attachments;
        }

        public TicketDTO TicketDto { get; }
        public IList<IFormFile> Attachments { get; }
    }
}