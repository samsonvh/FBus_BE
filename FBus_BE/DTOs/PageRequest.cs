using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FBus_BE.DTOs
{
    public class PageRequest
    {
        [Required]
        public int PageNumber { get; set; }
        [Required]
        public int PageSize { get; set; }
        public string? OrderBy { get; set; }
        public string? Direction { get; set; }
    }
}
