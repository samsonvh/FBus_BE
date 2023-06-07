using System;
using System.Collections.Generic;

namespace FBus_BE.Models;

public partial class Driver
{
    public short Id { get; set; }

    public short? AccountId { get; set; }

    public string FullName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string IdCardNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? PersonalEmail { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string? Avatar { get; set; }

    public DateTime CreatedDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual Account? Account { get; set; }

    public virtual ICollection<CoordinationStatus> CoordinationStatuses { get; set; } = new List<CoordinationStatus>();

    public virtual ICollection<Coordination> Coordinations { get; set; } = new List<Coordination>();
}
