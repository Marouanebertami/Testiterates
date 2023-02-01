using AutoMapper;
using Test_iterates.Entities;

namespace Test_iterates.Models.RequestDTO
{
    public class RequestProfile : Profile
    {
        public RequestProfile()
        {
            CreateMap<Request, RequestItemDTO>().ReverseMap();
        }
    }
}
