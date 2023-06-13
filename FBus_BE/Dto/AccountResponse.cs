using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Models;

namespace FBus_BE.Dto
{
    public class AccountResponse
    {
        public short Id { get; set; }

        public string Email { get; set; } = null!;

        public string? Password { get; set; }

        public string Code { get; set; } = null!;

        public string Role { get; set; } = null!;

        public string Status { get; set; } = null!;
        public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}