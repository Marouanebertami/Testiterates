using System.ComponentModel.DataAnnotations;

namespace Test_iterates.Models.BeerDTO
{
    public class SaveBeerDTO
    {

        [Required(ErrorMessage = "Champ obligatoir")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Champ obligatoir")]
        public long Id_brewery { get; set; }

        [Required(ErrorMessage = "Champ obligatoir")]
        public decimal Alcohol_Content { get; set; }

        [Required(ErrorMessage = "Champ obligatoir")]
        public decimal Price { get; set; }
    }
}
