using AutoMapper;
using Test_iterates.Entities;
using Test_iterates.Models.RequestDTO;

namespace Test_iterates.Services.ClientServices
{
    public class ClientServices : IClientServices
    {
        private readonly IMapper _mapper;
        private readonly BreweryWholesaleContext _breweryWholesalecontext;

        public ClientServices(IMapper mapper)
        {
            _breweryWholesalecontext = new BreweryWholesaleContext();
            _mapper = mapper;
        }

        public async Task<RequestReturnDTO> CreateRequest(SaveRequestDTO model)
        {
            RequestReturnDTO requestReturn = new RequestReturnDTO();

            if (model.listRequest == null)
            {
                requestReturn.messageError = "The order cannot be empty";
                return requestReturn;
            }

            Client client = _mapper.Map<Client>(model.client);

            if(OrderIsNull(model.listRequest))
            {
                requestReturn.messageError = "The order cannot be empty";
                return requestReturn;
            }

            // verify if request has duplicate order
            if (RequestDuplicated(model.listRequest))
            {
                requestReturn.messageError = "There can't be any duplicate in the order";
                return requestReturn;
            }

            // all wholesaler's exist in DB
            if (!WholersalerExist(model.listRequest.Select(r => r.IdWholesaler).ToList()))
            {
                requestReturn.messageError = "The wholesaler must exist";
                return requestReturn;
            }

            // The quantity exist in stock
            if (BeerExistInStock(model.listRequest))
            {
                requestReturn.messageError = "The number of beers ordered cannot be greater than the wholesaler's stock";
                return requestReturn;
            }

            Dictionary<long, decimal> listBeerPrice = GetBeersPrice(model.listRequest.Select(r => r.IdBeer).ToList());

            client.Requests = _mapper.Map<List<Request>>(model.listRequest);
            client.Requests.ToList().ForEach(r =>
            {
                if (listBeerPrice.ContainsKey(r.IdBeer))
                {
                    r.UnitPrice = listBeerPrice.First(br => br.Key == r.IdBeer).Value;
                }

                if (r.Quantity >= 10 && r.Quantity < 20)
                {
                    r.Discount = 10;
                }
                else if(r.Quantity >= 20 && listBeerPrice.ContainsKey(r.IdBeer))
                {
                    r.Discount = 10;
                }
            });

            await _breweryWholesalecontext.Clients.AddAsync(client);
            await _breweryWholesalecontext.SaveChangesAsync();

            requestReturn.name_client = client.NameComplete;
            requestReturn.phone = client.Phone;
            requestReturn.email = client.Email;

            requestReturn.listRequest = GetRequestReturn(client.Requests.ToList());

            requestReturn.total = requestReturn.listRequest.Sum(r => r.total_price);

            return requestReturn;
        }

        private Dictionary<long, decimal> GetBeersPrice(List<long?> idBeers)
        {
            return _breweryWholesalecontext.Beers.Where(b => idBeers.Contains(b.Id))
                                                 .Select(b => new { b.Id, b.Price })
                                                 .ToDictionary(b => b.Id, b => b.Price);

        }

        private bool WholersalerExist(List<long?> listIdWholeSaler)
        {
            List<long> listIdWholeSalerDb = _breweryWholesalecontext.Wholesalers.Where(w => listIdWholeSaler.Contains(w.Id))
                                                                                .Select(w => w.Id).ToList();

            foreach(long Id in listIdWholeSalerDb)
            {
                if (!listIdWholeSaler.Contains(Id))
                {
                    return false;
                }
            }

            return true;
        }

        private bool BeerExistInStock(List<RequestItemDTO> listRequest)
        {
            foreach (RequestItemDTO item in listRequest)
            {
                int? beerStock = _breweryWholesalecontext.WholesalerBeerStocks.Where(w => w.IdBeer == item.IdBeer && w.IdWholesaler == item.IdWholesaler)
                                                                             .Select(w => w.Quantity).FirstOrDefault();

                if(beerStock == null || beerStock < item.Quantity)
                {
                    return true;
                }
            }

            return false;
        }
    
        private bool RequestDuplicated(List<RequestItemDTO>? listRequest)
        {
            List<RequestItemDTO> listRequestChecked = new List<RequestItemDTO>();

            foreach(RequestItemDTO item in listRequest)
            {
                if (listRequestChecked.Any(r => r.IdBeer == item.IdBeer && r.IdWholesaler == item.IdBeer))
                {
                    return true;
                }

                listRequestChecked.Add(item);
            }

            return false;
        }
    
        private bool OrderIsNull(List<RequestItemDTO>? listRequest)
        {
            if (listRequest == null || listRequest.Count == 0)
            {
                return true;
            }

            foreach(RequestItemDTO item in listRequest)
            {
                if(item.Quantity == 0)
                {
                    return true;
                }
            }

            return false;
        }
    
        private List<RequestDTO> GetRequestReturn(List<Request> model)
        {
            List<RequestDTO> requestItems = new List<RequestDTO>();

            foreach(Request item in model)
            {
                requestItems.Add(new RequestDTO
                {
                    discount = item.Discount,
                    id_beer = item.IdBeer,
                    quantity = item.Quantity,
                    unit_price = item.UnitPrice,
                    total_price = (item.Quantity * item.UnitPrice) - (item.Quantity * item.UnitPrice * item.Discount / 100)
                });
            }

            return requestItems;
        }
    }
}
