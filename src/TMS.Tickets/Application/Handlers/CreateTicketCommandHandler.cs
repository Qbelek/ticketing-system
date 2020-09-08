using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TMS.Shared;
using TMS.Shared.DTO;
using TMS.Tickets.Integration.Commands;
using TMS.Tickets.Persistence;
using TMS.Tickets.Persistence.Rows;

namespace TMS.Tickets.Application.Handlers
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Response>
    {
        private readonly TicketsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly MachineDateTime _mdt;

        public CreateTicketCommandHandler(TicketsDbContext dbContext, IMapper mapper, MachineDateTime mdt)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _mdt = mdt;
        }

        public async Task<Response> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticketRow = _mapper.Map<TicketRow>(request.TicketDto);
            _dbContext.Tickets.Add(ticketRow);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new Response(_mapper.Map<TicketDTO>(ticketRow), _mdt);
        }
    }
}