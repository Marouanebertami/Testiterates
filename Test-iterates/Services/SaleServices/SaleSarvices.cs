using AutoMapper;
using Test_iterates.Entities;
using Test_iterates.Models.WholesalerBeerStockDTO;

namespace Test_iterates.Services.SaleServices
{
    public class SaleServices : ISaleServices
    {
        private readonly IMapper _mapper;
        private readonly BreweryWholesaleContext _breweryWholesalecontext;
        public SaleServices(IMapper mapper)
        {
            _breweryWholesalecontext = new BreweryWholesaleContext();
            _mapper = mapper;
        }

        public async Task<string> AddSale(SaveWholesalerBeerStockDTO model)
        {
            if (!model.Id_Beer.HasValue || !_breweryWholesalecontext.Beers.Any(b => b.Id == model.Id_Beer))
            {
                return "Beer N°: " + model.Id_Beer + " not Exist";
            }

            if(!model.Id_Beer.HasValue || !_breweryWholesalecontext.Wholesalers.Any(w => w.Id == model.Id_Wholesaler))
            {
                return "Wholesaler N°: " + model.Id_Wholesaler + " not Exist";
            }

            WholesalerBeerStock? wholeSalerBeerStockExist = _breweryWholesalecontext.WholesalerBeerStocks
                                                                                   .FirstOrDefault(w => w.IdWholesaler == model.Id_Wholesaler
                                                                                                     && w.IdBeer == model.Id_Beer);
            if(wholeSalerBeerStockExist != null)
            {
                return "beer exist with the same Wholesaler";
            }

            WholesalerBeerStock wholesalerBeerStock = _mapper.Map<WholesalerBeerStock>(model);
            _breweryWholesalecontext.WholesalerBeerStocks.Add(wholesalerBeerStock);

            await _breweryWholesalecontext.SaveChangesAsync();

            return "";
        }

        public async Task<string> UpdateStock(SaveWholesalerBeerStockDTO model)
        {
            if (!model.Id_Beer.HasValue || !_breweryWholesalecontext.Beers.Any(b => b.Id == model.Id_Beer))
                return "Beer N°: " + model.Id_Beer + " not Exist";

            if (!model.Id_Beer.HasValue || !_breweryWholesalecontext.Wholesalers.Any(w => w.Id == model.Id_Wholesaler))
                return "Wholesaler N°: " + model.Id_Wholesaler + " not Exist";

            WholesalerBeerStock? wholeSalerBeerStockExist = _breweryWholesalecontext.WholesalerBeerStocks
                                                                                   .FirstOrDefault(w => w.IdWholesaler == model.Id_Wholesaler
                                                                                                     && w.IdBeer == model.Id_Beer);

            if (wholeSalerBeerStockExist != null)
                wholeSalerBeerStockExist.Quantity = model.quantity.Value;

            await _breweryWholesalecontext.SaveChangesAsync();

            return "";
        }
    }
}
