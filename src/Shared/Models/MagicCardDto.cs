using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MagicStock.Shared.Models;

/// <summary>
/// DTO principal para las cartas Magic: The Gathering
/// Compatible con la API de Scryfall y el almacenamiento local
/// </summary>
public sealed class MagicCardDto
{
    [Required]
    [JsonPropertyName("id")]
    public required string Id { get; init; }
    
    [Required, MaxLength(200)]
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    
    [JsonPropertyName("set")]
    public required string SetCode { get; init; }
    
    [JsonPropertyName("set_name")]
    public required string SetName { get; init; }
    
    [JsonPropertyName("rarity")]
    public required string Rarity { get; init; }
    
    [JsonPropertyName("prices")]
    public CardPricesDto? Prices { get; init; }
    
    [JsonPropertyName("type_line")]
    public required string TypeLine { get; init; }
    
    [JsonPropertyName("mana_cost")]
    public string? ManaCost { get; init; }
    
    [JsonPropertyName("cmc")]
    public int? ConvertedManaCost { get; init; }
    
    [JsonPropertyName("colors")]
    public string[]? Colors { get; init; }
    
    [JsonPropertyName("color_identity")]
    public string[]? ColorIdentity { get; init; }
    
    [JsonPropertyName("oracle_text")]
    public string? OracleText { get; init; }
    
    [JsonPropertyName("flavor_text")]
    public string? FlavorText { get; init; }
    
    [JsonPropertyName("artist")]
    public string? Artist { get; init; }
    
    [JsonPropertyName("released_at")]
    public DateTime ReleasedAt { get; init; }
    
    [JsonPropertyName("image_uris")]
    public CardImageUrisDto? ImageUris { get; init; }
    
    [JsonPropertyName("reserved")]
    public bool IsReserved { get; init; }
    
    [JsonPropertyName("collector_number")]
    public required string CollectorNumber { get; init; }
    
    // Propiedades locales para gestión de stock
    public int StockQuantity { get; set; }
    public decimal? AcquisitionPrice { get; set; }
    public string Condition { get; set; } = CardConditions.NearMint;
    public DateTime? DateAdded { get; set; }
    public string? Notes { get; set; }
    
    /// <summary>
    /// Precio USD actual de la carta
    /// </summary>
    public decimal? CurrentPrice => Prices?.Usd;
    
    /// <summary>
    /// URL de la imagen principal
    /// </summary>
    public string? ImageUrl => ImageUris?.Normal ?? ImageUris?.Large ?? ImageUris?.Small;
    
    /// <summary>
    /// Valor total del stock de esta carta
    /// </summary>
    public decimal TotalStockValue => (CurrentPrice ?? 0) * StockQuantity;
}

public sealed class CardPricesDto
{
    [JsonPropertyName("usd")]
    public decimal? Usd { get; init; }
    
    [JsonPropertyName("usd_foil")]
    public decimal? UsdFoil { get; init; }
    
    [JsonPropertyName("eur")]
    public decimal? Eur { get; init; }
}

public sealed class CardImageUrisDto
{
    [JsonPropertyName("small")]
    public string? Small { get; init; }
    
    [JsonPropertyName("normal")]
    public string? Normal { get; init; }
    
    [JsonPropertyName("large")]
    public string? Large { get; init; }
    
    [JsonPropertyName("png")]
    public string? Png { get; init; }
    
    [JsonPropertyName("art_crop")]
    public string? ArtCrop { get; init; }
    
    [JsonPropertyName("border_crop")]
    public string? BorderCrop { get; init; }
}

/// <summary>
/// Constantes para las condiciones de las cartas
/// </summary>
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
    
    public static readonly Dictionary<string, string> Descriptions = new()
    {
        { Mint, "Perfecta condición, sin uso" },
        { NearMint, "Casi perfecta, uso mínimo" },
        { LightlyPlayed, "Ligeros signos de uso" },
        { ModeratelyPlayed, "Signos moderados de uso" },
        { HeavilyPlayed, "Signos evidentes de uso" },
        { Damaged, "Daños visibles" }
    };
} 