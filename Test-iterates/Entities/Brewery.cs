using System;
using System.Collections.Generic;

namespace Test_iterates.Entities
{
    public partial class Brewery
    {
        public Brewery()
        {
            Beers = new HashSet<Beer>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Beer> Beers { get; set; }
    }
}
