using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TMS.Shared;
using TMS.Shared.ApiErrors;
using TMS.Tickets.Integration.Queries;

namespace TMS.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : BaseController
    {
        public TicketsController(IMediator mediator) : base(mediator)
        { }
        
        /// <summary>
        /// Returns ticket with given ID
        /// </summary>
        /// <remarks>
        /// Regular user can query for tickets created by him.
        /// ItWorker can query for tickets created by him and for tickets assigned to him.
        /// Admin can query for every ticket.
        /// </remarks>
        /// <param name="id">Guid</param>
        /// <returns>Ticket</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(new BadRequestError(InvalidGuidMessage));
            }
            
            var response = await Mediator.Send(new GetTicketQuery(id.Value));
            return ProcessResponse(response);
        }
    }
}