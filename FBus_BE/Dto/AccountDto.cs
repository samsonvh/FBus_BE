using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBus_BE.Dto
{
    public class AccountDto
    {
        public short Id { get; set; }

        public string Email { get; set; } = null!;

        public string? Password { get; set; }

        public string Code { get; set; } = null!;

        public string Role { get; set; } = null!;

        public string Status { get; set; } = null!;
    }
}