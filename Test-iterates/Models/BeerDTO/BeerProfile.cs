using AutoMapper;
using Test_iterates.Entities;

namespace Test_iterates.Models.BeerDTO
{
    public class BeerProfile : Profile
    {
        public BeerProfile()
        {
            CreateMap<Beer, BeerItem>()
                .ForMember(b => b.Name_Brewery, x => x.MapFrom(b => b.IdBreweryNavigation.Name))
                .ForMember(b => b.Id_Brewery, x => x.MapFrom(b => b.IdBrewery))
                .ForMember(b => b.Alcohol_Content, x => x.MapFrom(b => b.AlcoholContent))
                .ReverseMap();

            CreateMap<Beer, SaveBeerDTO>()
                .ForMember(b => b.Id_brewery, x => x.MapFrom(b => b.IdBrewery))
                .ForMember(b => b.Alcohol_Content, x => x.MapFrom(b => b.AlcoholContent))
                .ReverseMap();
        }
    }
}
