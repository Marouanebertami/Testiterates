using System;
using System.Collections.Generic;

namespace Test_iterates.Entities
{
    public partial class Request
    {
        public long Id { get; set; }
        public long IdWholesaler { get; set; }
        public long IdBeer { get; set; }
        public long IdClient { get; set; }
        public int Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Discount { get; set; }

        public virtual Beer IdBeerNavigation { get; set; } = null!;
        public virtual Client IdClientNavigation { get; set; } = null!;
        public virtual Wholesaler IdWholesalerNavigation { get; set; } = null!;
    }
}
