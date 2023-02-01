using Test_iterates.Entities;
using Test_iterates.Models.BeerDTO;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Reflection;
using System.Resources;

namespace Test_iterates.Services.BeerServices
{
    public class BeerServices : IBeerServices
    {
        private readonly IMapper _mapper;
        private readonly BreweryWholesaleContext _breweryWholesalecontext;

        public BeerServices(IMapper mapper)
        {
            _breweryWholesalecontext = new BreweryWholesaleContext();
            _mapper = mapper;
        }

        public async Task<List<Beer>> GetListBeerByBrewery(long id_brewery)
        {
            return await _breweryWholesalecontext.Beers.Include(b => b.IdBreweryNavigation)
                                                           .Where(b => b.IdBrewery == id_brewery).ToListAsync();
        }

        public async Task<BeerItem> AddBeer(SaveBeerDTO model)
        {
            Beer beer = _mapper.Map<Beer>(model);

            _breweryWholesalecontext.Beers.Add(beer);
            await _breweryWholesalecontext.SaveChangesAsync();

            return _mapper.Map<Beer, BeerItem>(beer);
        }

        public async Task<string?> DeleteBeer(long id_beer)
        {
            Beer? beer = await _breweryWholesalecontext.Beers.Include(b => b.WholesalerBeerStocks).FirstOrDefaultAsync(b => b.Id == id_beer);

            if(beer == null)
            {
                return string.Format("There's no beer whith Number {0}", id_beer);
            }

            if (beer.WholesalerBeerStocks.Any())
            {
                return "Beer can't be deleted, is used in the app";
            }

            _breweryWholesalecontext.Beers.Remove(beer);
            await _breweryWholesalecontext.SaveChangesAsync();
            return null;
        }
    }
}
