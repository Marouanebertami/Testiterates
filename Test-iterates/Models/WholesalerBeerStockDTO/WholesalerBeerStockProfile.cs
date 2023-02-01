using AutoMapper;
using Test_iterates.Entities;

namespace Test_iterates.Models.WholesalerBeerStockDTO
{
    public class WholesalerBeerStockProfile : Profile
    {
        public WholesalerBeerStockProfile()
        {
            CreateMap<WholesalerBeerStock, SaveWholesalerBeerStockDTO>()
                .ForMember(b => b.Id_Beer, x => x.MapFrom(b => b.IdBeer))
                .ForMember(b => b.Id_Wholesaler, x => x.MapFrom(b => b.IdWholesaler))
                .ForMember(b => b.quantity, x => x.MapFrom(b => b.Quantity))
                .ReverseMap();

            //CreateMap<Beer, SaveBeerDTO>()
            //    .ForMember(b => b.Id_brewery, x => x.MapFrom(b => b.IdBrewery))
            //    .ForMember(b => b.Alcohol_Content, x => x.MapFrom(b => b.AlcoholContent))
            //    .ReverseMap();
        }
    }
}
