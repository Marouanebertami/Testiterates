using System;
using System.Collections.Generic;

namespace Test_iterates.Entities
{
    public partial class Client
    {
        public Client()
        {
            Requests = new HashSet<Request>();
        }

        public long Id { get; set; }
        public string NameComplete { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Request> Requests { get; set; }
    }
}
