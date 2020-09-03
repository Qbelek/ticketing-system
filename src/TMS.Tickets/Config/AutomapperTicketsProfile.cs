using AutoMapper;
using TMS.Shared.DTO;
using TMS.Tickets.Persistence.Rows;

namespace TMS.Tickets.Config
{
    public class AutomapperTicketsProfile : Profile
    {
        public AutomapperTicketsProfile()
        {
            CreateMap<UserRow, UserDTO>();
            CreateMap<UserDTO, UserRow>();
            CreateMap<TicketRow, TicketDTO>();
            CreateMap<TicketDTO, TicketRow>();
        }
    }
}