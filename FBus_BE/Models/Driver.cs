using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FBus_BE.Models;

public class Driver
{
    [Key]
    public short Id { get; set; }

    public short? AccountId { get; set; }
    [Required]
    [StringLength(50)]
    public string FullName { get; set; } = null!;
    [Required]
    [StringLength(6)]
    public string Gender { get; set; } = null!;
    [Required]
    [StringLength(12)]
    public string IdCardNumber { get; set; } = null!;
    [Required]
    [StringLength(100)]
    public string Address { get; set; } = null!;
    [Required]
    [StringLength(13)]
    [RegularExpression(@"^[0-9]+$", ErrorMessage = "Phone number must be numeric.")]
    public string PhoneNumber { get; set; } = null!;
    [StringLength(100)]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string? PersonalEmail { get; set; }

    public DateTime DateOfBirth { get; set; }
    [StringLength(100)]
    public string? Avatar { get; set; }

    public DateTime CreatedDate { get; set; }
    [Required]
    [StringLength(15)]
    public string Status { get; set; } = null!;
    public virtual Account? Account { get; set; }
    public virtual ICollection<CoordinationStatus> CoordinationStatuses { get; set; } = new List<CoordinationStatus>();
    public virtual ICollection<Coordination> Coordinations { get; set; } = new List<Coordination>();
}
