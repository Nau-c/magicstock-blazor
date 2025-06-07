namespace MagicStock.Shared.Models;

public class CardSearchFilters
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Rarity { get; set; }
    public string? Set { get; set; }
    public string? Color { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? MinQuantity { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
} 