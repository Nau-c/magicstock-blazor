namespace MagicStock.Shared.DTOs;

public class StockStatsDto
{
    public int TotalCards { get; set; }
    public int UniqueCards { get; set; }
    public decimal TotalValue { get; set; }
    public decimal AverageCardValue { get; set; }
    public Dictionary<string, int> RarityDistribution { get; set; } = new();
    public Dictionary<string, int> ColorDistribution { get; set; } = new();
    public Dictionary<string, int> SetDistribution { get; set; } = new();
    public Dictionary<string, decimal> ValueByRarity { get; set; } = new();
    public DateTime LastUpdated { get; set; }
} 