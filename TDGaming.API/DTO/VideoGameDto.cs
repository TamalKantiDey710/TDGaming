using System.ComponentModel.DataAnnotations;

namespace TDGaming.API.DTO;

public class VideoGameDto
{
    public System.Guid Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Price must be at least 0")]
    public decimal Price { get; set; }
}
