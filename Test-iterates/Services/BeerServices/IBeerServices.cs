using Test_iterates.Entities;
using Test_iterates.Models.BeerDTO;

namespace Test_iterates.Services.BeerServices
{
    public interface IBeerServices
    {
        public Task<List<Beer>> GetListBeerByBrewery(long id_brewery);

        public Task<BeerItem> AddBeer(SaveBeerDTO model);

        public Task<string?> DeleteBeer(long id_beer);
    }
}
