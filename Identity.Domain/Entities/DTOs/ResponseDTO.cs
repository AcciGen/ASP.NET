using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities.DTOs
{
    public class ResponseDTO
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; } = false;
    }
}
