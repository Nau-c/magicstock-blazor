using System.ComponentModel.DataAnnotations;

namespace MagicStock.Shared.Models;

public class Card
{
    public string Id { get; set; } = string.Empty;
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public string? ManaCost { get; set; }
    
    public string? Type { get; set; }
    
    public string? Rarity { get; set; }
    
    public string? Set { get; set; }
    
    public decimal Price { get; set; }
    
    public int Quantity { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
} 