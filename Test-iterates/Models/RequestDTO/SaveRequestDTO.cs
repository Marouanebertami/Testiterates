using Test_iterates.Models.ClientDTO;

namespace Test_iterates.Models.RequestDTO
{
    public class SaveRequestDTO
    {
        public ClientItemDTO? client { get; set; }

        public List<RequestItemDTO>? listRequest { get; set; }
    }
}
