using System;
using System.Collections.Generic;

namespace TeamManagement.Models
{
    public class Team
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Membership> Memberships { get; set; }
    }
}
