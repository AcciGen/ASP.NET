using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities.DTOs
{
    public class RegistrationDTO
    {
        public required string FullName { get; set; }
        public required string UserName { get; set; }
        public int Age { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
        public string Status { get; set; }

        public IList<string> Roles { get; set; }
    }
}
