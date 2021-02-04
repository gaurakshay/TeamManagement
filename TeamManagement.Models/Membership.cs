using System;
using SQLite;

namespace TeamManagement.Models
{
    public class Membership
    {
        public Guid ID { get; set; }
        public Guid TeamID { get; set; }
        public Guid EmployeeID { get; set; }
        [Ignore]
        public Team Team { get; set; }
        [Ignore]
        public Employee Employee { get; set; }
    }
}
