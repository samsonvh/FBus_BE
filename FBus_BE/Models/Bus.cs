using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FBus_BE.Models;

public class Bus
{
    [Key]
    public short Id { get; set; }

    public short? CreatedById { get; set; }

    public string Code { get; set; } = null!;

    public string LicensePlate { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string Color { get; set; } = null!;

    public byte Seat { get; set; }

    public DateTime? DateOfRegistration { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Coordination> Coordinations { get; set; }

    public virtual Account? CreatedBy { get; set; }
}
