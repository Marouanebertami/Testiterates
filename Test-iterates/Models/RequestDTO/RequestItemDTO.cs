using System.ComponentModel.DataAnnotations;

namespace Test_iterates.Models.RequestDTO
{
    public class RequestItemDTO
    {
        [Required(ErrorMessage = "The beer must be sold by the wholesaler")]
        public long? IdWholesaler { get; set; }

        [Required(ErrorMessage = "Champ obligatoir")]
        public long? IdBeer { get; set; }

        [Required(ErrorMessage = "Champ obligatoir")]
        public int? Quantity { get; set; }
    }

    public class RequestReturnDTO
    {
        public string? messageError { get; set; }
        public string? name_client { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public List<RequestDTO>? listRequest { get; set; }
        public decimal? total { get; set; }
    }

    public class RequestDTO
    {
        public long? id_beer { get; set; }
        public int? quantity { get; set; }
        public decimal? unit_price { get; set; }
        public decimal? discount { get; set; }
        public decimal? total_price { get; set; }
    }
}
