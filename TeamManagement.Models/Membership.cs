using System;

namespace TeamManagement.Models
{
    public class Membership
    {
        public Guid ID { get; set; }
        public Guid TeamID { get; set; }
        public Guid EmployeeID { get; set; }

        public Team Team { get; set; }
        public Employee Employee { get; set; }
    }
}
