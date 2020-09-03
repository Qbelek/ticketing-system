using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TMS.Shared;
using TMS.Shared.ApiErrors;
using TMS.Shared.DTO;
using TMS.Tickets.Integration.Queries;
using TMS.Tickets.Persistence;

namespace TMS.Tickets.Application.Handlers
{
    public class GetTicketQueryHandler : IRequestHandler<GetTicketQuery, Response<TicketDTO>>
    {
        private readonly TicketsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly MachineDateTime _mdt;

        public GetTicketQueryHandler(TicketsDbContext dbContext, IMapper mapper, MachineDateTime mdt)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _mdt = mdt;
        }

        public async Task<Response<TicketDTO>> Handle(GetTicketQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _dbContext.Tickets
                .Where(x => x.Id == request.Id)
                .ProjectTo<TicketDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (ticket == null)
                return new Response<TicketDTO>(new NotFoundError("Ticket with this ID does not exist"), _mdt);

            return new Response<TicketDTO>(ticket, _mdt);
        }
    }
}