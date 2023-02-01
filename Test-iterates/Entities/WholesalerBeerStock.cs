using System;
using System.Collections.Generic;

namespace Test_iterates.Entities
{
    public partial class WholesalerBeerStock
    {
        public long Id { get; set; }
        public long IdBeer { get; set; }
        public long IdWholesaler { get; set; }
        public int Quantity { get; set; }

        public virtual Beer IdBeerNavigation { get; set; } = null!;
        public virtual Wholesaler IdWholesalerNavigation { get; set; } = null!;
    }
}
