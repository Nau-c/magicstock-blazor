namespace MagicStock.Shared.Models;

public class MagicSet
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public string? IconUrl { get; set; }
    public int CardCount { get; set; }
    public string Type { get; set; } = string.Empty;
} 