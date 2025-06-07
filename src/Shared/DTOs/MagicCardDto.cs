namespace MagicStock.Shared.DTOs;

public class MagicCardDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string SetCode { get; set; } = string.Empty;
    public string SetName { get; set; } = string.Empty;
    public string Rarity { get; set; } = string.Empty;
    public CardPricesDto? Prices { get; set; }
    public string TypeLine { get; set; } = string.Empty;
    public string CollectorNumber { get; set; } = string.Empty;
    public DateTime ReleasedAt { get; set; }
    public CardImageUrisDto? ImageUris { get; set; }
    public bool IsReserved { get; set; }
    public int StockQuantity { get; set; }
    public string? ImageUrl => ImageUris?.Normal;
    public decimal? CurrentPrice => Prices?.Usd;
}

public class CardPricesDto
{
    public decimal? Usd { get; set; }
    public decimal? UsdFoil { get; set; }
    public decimal? Eur { get; set; }
    public decimal? EurFoil { get; set; }
}

public class CardImageUrisDto
{
    public string? Small { get; set; }
    public string? Normal { get; set; }
    public string? Large { get; set; }
    public string? Png { get; set; }
    public string? ArtCrop { get; set; }
    public string? BorderCrop { get; set; }
} 