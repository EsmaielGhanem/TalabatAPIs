using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs;

public class BasketItemDto
{
    [Required]

    public int Id { get; set; }
    [Required]

    public string Name  { get; set; }
    [Required]

    
    public string Description  { get; set; }
    [Required]
    [Range(1 , int.MaxValue , ErrorMessage = "Quantity must be in range")]

    public int  Quantity { get; set; }
    [Required]
    [Range(0.1 , double.MaxValue , ErrorMessage = "The Price Must be greater than ZERO") ]

    public decimal Price { get; set; }
    [Required]

    public string PictureUrl { get; set; }
    [Required]

    public string  Brand { get; set; }
    [Required]

    public string  Type { get; set; }

}