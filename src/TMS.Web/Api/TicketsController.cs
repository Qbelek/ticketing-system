using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS.Shared;
using TMS.Shared.ApiErrors;
using TMS.Shared.DTO;
using TMS.Tickets.Integration.Commands;
using TMS.Tickets.Integration.Queries;
using TMS.Web.Binders;
using TMS.Web.Requests;

namespace TMS.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : BaseController
    {
        public TicketsController(IMediator mediator, IMapper mapper, MachineDateTime mdt) : base(mediator, mapper, mdt)
        {
        }

        /// <summary>
        ///     Returns ticket with given ID
        /// </summary>
        /// <remarks>
        ///     Regular user can query for tickets created by him.
        ///     ItWorker can query for tickets created by him and for tickets assigned to him.
        ///     Admin can query for every ticket.
        /// </remarks>
        /// <param name="ticketId">Guid</param>
        /// <returns>Ticket</returns>
        [HttpGet("{ticketId}")]
        public async Task<IActionResult> Get([FromRoute] Guid? ticketId)
        {
            Response response;

            if (!ticketId.HasValue)
            {
                response = new Response(new BadRequestError(InvalidGuidMessage), Mdt);
                return ProcessResponse(response);
            }

            response = await Mediator.Send(new GetTicketQuery(ticketId.Value));
            return ProcessResponse(response);
        }


        [HttpPost("create-ticket")]
        public async Task<IActionResult> CreateTicket(
            [ModelBinder(BinderType = typeof(JsonWithFilesModelBinder))]
            CreateTicketRequest request,
            IList<IFormFile> attachments)
        {
            if (request == null)
            {
                return ProcessResponse(new Response(new BadRequestError("Model binding failed."), Mdt));
            }

            var command = new CreateTicketCommand(Mapper.Map<TicketDTO>(request), attachments);
            var response = await Mediator.Send(command);
            return ProcessResponse(response);
        }
    }
}