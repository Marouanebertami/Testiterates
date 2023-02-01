using Test_iterates.Models.WholesalerBeerStockDTO;

namespace Test_iterates.Services.SaleServices
{
    public interface ISaleServices
    {
        public Task<string> AddSale(SaveWholesalerBeerStockDTO model);

        public Task<string> UpdateStock(SaveWholesalerBeerStockDTO model);
    }
}
