namespace Test_iterates.Models.BeerDTO
{
    public class BeerItem
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public long Id_Brewery { get; set; }
        public string? Name_Brewery { get; set; }
        public decimal Price { get; set; }
        public decimal Alcohol_Content { get; set; }
    }

    public class BeerItemPice
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
