using System;
using System.Collections.Generic;

namespace Test_iterates.Entities
{
    public partial class Beer
    {
        public Beer()
        {
            Requests = new HashSet<Request>();
            WholesalerBeerStocks = new HashSet<WholesalerBeerStock>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal AlcoholContent { get; set; }
        public decimal Price { get; set; }
        public long IdBrewery { get; set; }

        public virtual Brewery IdBreweryNavigation { get; set; } = null!;
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<WholesalerBeerStock> WholesalerBeerStocks { get; set; }
    }
}
