using System.ComponentModel.DataAnnotations;

namespace MagicStock.Core.Entities;

public sealed class Card
{
    public required string Id { get; init; }
    
    [Required, MaxLength(200)]
    public required string Name { get; init; }
    
    public required string SetCode { get; init; }
    
    public required string SetName { get; init; }
    
    public required string Rarity { get; init; }
    
    public required decimal? Price { get; init; }
    
    public required string TypeLine { get; init; }
    
    public required string? ManaCost { get; init; }
    
    public required int? ConvertedManaCost { get; init; }
    
    public required string[]? Colors { get; init; }
    
    public required string[]? ColorIdentity { get; init; }
    
    public required string? OracleText { get; init; }
    
    public required string? FlavorText { get; init; }
    
    public required string? Artist { get; init; }
    
    public required DateTime ReleasedAt { get; init; }
    
    public required CardImages Images { get; init; }
    
    public required bool IsReserved { get; init; }
    
    public required string CollectorNumber { get; init; }
    
    // Propiedades del stock local
    public int StockQuantity { get; set; }
    
    public decimal? AcquisitionPrice { get; set; }
    
    public string? Condition { get; set; } = "Near Mint";
    
    public DateTime? DateAdded { get; set; }
    
    public string? Notes { get; set; }
}

public sealed record CardImages(
    string? Small,
    string? Normal,
    string? Large,
    string? Png,
    string? ArtCrop,
    string? BorderCrop
);

public static class CardConditions
{
    public const string Mint = "Mint";
    public const string NearMint = "Near Mint";
    public const string LightlyPlayed = "Lightly Played";
    public const string ModeratelyPlayed = "Moderately Played";
    public const string HeavilyPlayed = "Heavily Played";
    public const string Damaged = "Damaged";
    
    public static readonly string[] All = 
    {
        Mint, NearMint, LightlyPlayed, 
        ModeratelyPlayed, HeavilyPlayed, Damaged
    };
} 