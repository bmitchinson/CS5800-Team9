using System.Collections.Generic;

namespace backend.Data.Models
{
    public class Administrator
    {
        public int AdministratorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}