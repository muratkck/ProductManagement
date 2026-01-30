using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProductManagement.DTOs
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Product name is required")]
        [MaxLength(200, ErrorMessage = "Product name cannot exceed 200 characters")]
        [DefaultValue("Gaming Laptop")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        [DefaultValue("High performance laptop with RTX 4080 GPU")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be between 0.01 and 999,999.99")]
        [DefaultValue(1599.99)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
        [DefaultValue(25)]
        public int Stock { get; set; }
    }
}