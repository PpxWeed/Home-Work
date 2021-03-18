using AutoMapper;
using DTO;
using Models;

namespace Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Reservation, ReservationModel>();
            CreateMap<RegisterReserv, Reservation>();
            CreateMap<Reserv_Modif, Reservation>();
        }
    }
}