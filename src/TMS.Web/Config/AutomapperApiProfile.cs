using AutoMapper;
using TMS.Shared.DTO;
using TMS.Web.Requests;

namespace TMS.Web.Config
{
    public class AutomapperApiProfile : Profile
    {
        public AutomapperApiProfile()
        {
            CreateMap<CreateTicketRequest, TicketDTO>();
        }
    }
}