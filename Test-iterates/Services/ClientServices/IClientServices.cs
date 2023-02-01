using Test_iterates.Models.RequestDTO;

namespace Test_iterates.Services.ClientServices
{
    public interface IClientServices
    {
        public Task<RequestReturnDTO> CreateRequest(SaveRequestDTO model);
    }
}
