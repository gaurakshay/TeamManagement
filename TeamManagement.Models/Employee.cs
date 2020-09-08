using System;

namespace TeamManagement.Models
{
    public class Employee
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Membership Membership { get; set; }
    }
}
