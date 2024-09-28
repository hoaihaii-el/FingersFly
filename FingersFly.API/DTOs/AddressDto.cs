using System.ComponentModel.DataAnnotations;

namespace FingersFly.API.DTOs
{
    public class AddressDto
    {
        [Required]
        public string Line1 { get; set; } = "";
        [Required]
        public string? Line2 { get; set; }
        [Required]
        public string City { get; set; } = "";
        [Required]
        public string PostalCode { get; set; } = "";
        [Required]
        public string Country { get; set; } = "";
    }
}
