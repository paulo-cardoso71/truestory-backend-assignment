using System.ComponentModel.DataAnnotations;

namespace TruestoryApi.Models
{
    public class Product
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Data { get; set; }
    }
}