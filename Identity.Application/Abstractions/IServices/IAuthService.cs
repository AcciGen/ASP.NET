using Identity.Domain.Entities.DTOs;
using Identity.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Abstractions.IServices
{
    public interface IAuthService
    {
        public Task<AuthResponseDTO> GenerateToken(AppUser user);
    }
}
