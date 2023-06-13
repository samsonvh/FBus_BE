using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FBus_BE.Models;

public class CoordinationStatus
{
    [Key]
    public short Id { get; set; }

    public short? CoordinationId { get; set; }

    public short? DriverId { get; set; }

    public string OriginalStatus { get; set; } = null!;

    public string UpdatedStatus { get; set; } = null!;

    public byte StatusOrder { get; set; }

    public string? Note { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Coordination? Coordination { get; set; }

    public virtual Driver? Driver { get; set; }
}
