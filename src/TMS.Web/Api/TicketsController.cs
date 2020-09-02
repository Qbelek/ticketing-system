using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TMS.Shared;
using TMS.Shared.ApiErrors;
using TMS.Shared.DTO;
using TMS.Tickets.Integration.Queries;

namespace TMS.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : BaseController
    {
        public TicketsController(IMediator mediator, MachineDateTime mdt) : base(mediator, mdt)
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
        /// <param name="id">Guid</param>
        /// <returns>Ticket</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid? id)
        {
            Response<TicketDTO> response;

            if (!id.HasValue)
            {
                response = new Response<TicketDTO>(new BadRequestError(InvalidGuidMessage), _mdt);
                return ProcessResponse(response);
            }

            response = await Mediator.Send(new GetTicketQuery(id.Value));
            return ProcessResponse(response);
        }
    }
}