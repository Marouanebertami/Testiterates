using AutoMapper;
using Test_iterates.Entities;

namespace Test_iterates.Models.ClientDTO
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientItemDTO>().ReverseMap();
        }
    }
}
