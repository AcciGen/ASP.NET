using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities.DTOs
{
    public class RoleDTO
    {
        public required string RoleName { get; set; } = String.Empty;
    }
}
