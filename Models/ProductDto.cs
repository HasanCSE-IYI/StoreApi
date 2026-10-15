using System.ComponentModel.DataAnnotations;

namespace StoreApi.Models;

public class ProductCreateDto
{
    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
    public required string Name { get; set; }

    public string? Category { get; set; }

    [Range(0.01, 1000000.00, ErrorMessage = "Price must be greater than 0.")]
    public decimal Price { get; set; }

    [Range(0, 10000, ErrorMessage = "Stock cannot be negative.")]
    public int Stock { get; set; }

    public string? ImageUrl { get; set; }
}

public class ProductUpdateDto : ProductCreateDto { }