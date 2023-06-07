using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FBus_BE.Dto
{
    public class DriverDto
    {
        public short Id { get; set; }
        public short? AccountId { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string IdCardNumber { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string PersonalEmail { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Avatar { get; set; }
        public string Status { get; set; }
    }
}