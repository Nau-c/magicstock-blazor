using System.Text.Json.Serialization;

namespace MagicStock.Shared.Models;

/// <summary>
/// DTO para entrada de stock de cartas
/// </summary>
public sealed record StockEntryDto
{
    public required string CardId { get; init; }
    public required int Quantity { get; init; }
    public required decimal? AcquisitionPrice { get; init; }
    public required string Condition { get; init; }
    public required DateTime DateAdded { get; init; }
    public string? Notes { get; init; }
}

/// <summary>
/// DTO para estadísticas del stock
/// </summary>
public sealed record StockStatsDto
{
    public required int TotalCards { get; init; }
    public required int UniqueCards { get; init; }
    public required decimal TotalValue { get; init; }
    public required decimal AverageCardValue { get; init; }
    public required Dictionary<string, int> RarityDistribution { get; init; }
    public required Dictionary<string, int> ColorDistribution { get; init; }
    public required Dictionary<string, int> SetDistribution { get; init; }
    public required Dictionary<string, decimal> ValueByRarity { get; init; }
    public required DateTime LastUpdated { get; init; }
}

/// <summary>
/// Resultado paginado genérico
/// </summary>
public sealed record PaginatedResultDto<T>
{
    public required IEnumerable<T> Data { get; init; }
    public required int TotalCount { get; init; }
    public required int Page { get; init; }
    public required int PageSize { get; init; }
    public required bool HasMore { get; init; }
    
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}

/// <summary>
/// Respuesta estándar de API
/// </summary>
public sealed record ApiResponseDto<T>
{
    public required bool Success { get; init; }
    public T? Data { get; init; }
    public string? Message { get; init; }
    public string? Error { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    
    public static ApiResponseDto<T> Ok(T data, string? message = null) => new()
    {
        Success = true,
        Data = data,
        Message = message
    };
    
    public static ApiResponseDto<T> Fail(string error) => new()
    {
        Success = false,
        Error = error
    };
}

/// <summary>
/// Filtros de búsqueda para cartas
/// </summary>
public sealed record CardSearchFiltersDto
{
    public string? Name { get; init; }
    public string? SetCode { get; init; }
    public string? Rarity { get; init; }
    public string[]? Colors { get; init; }
    public string[]? ColorIdentity { get; init; }
    public string? Type { get; init; }
    public string? Artist { get; init; }
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
    public int? MinCmc { get; init; }
    public int? MaxCmc { get; init; }
    public bool? IsReserved { get; init; }
    public bool? OnlyInStock { get; init; }
    public string SortBy { get; init; } = "name";
    public string SortDirection { get; init; } = "asc";
    
    /// <summary>
    /// Convierte a query string de Scryfall
    /// </summary>
    public string ToScryfallQuery()
    {
        var queryParts = new List<string>();
        
        if (!string.IsNullOrWhiteSpace(Name))
        {
            // Escapar comillas y caracteres especiales
            var escapedName = Name.Replace("\"", "\\\"");
            queryParts.Add($"name:\"{escapedName}\"");
        }
            
        if (!string.IsNullOrWhiteSpace(SetCode))
            queryParts.Add($"set:{SetCode}");
            
        if (!string.IsNullOrWhiteSpace(Rarity))
            queryParts.Add($"rarity:{Rarity}");
            
        if (Colors?.Any() == true)
            queryParts.Add($"color:{string.Join("", Colors)}");
            
        if (!string.IsNullOrWhiteSpace(Type))
            queryParts.Add($"type:\"{Type}\"");
            
        if (!string.IsNullOrWhiteSpace(Artist))
            queryParts.Add($"artist:\"{Artist}\"");
            
        if (MinPrice.HasValue)
            queryParts.Add($"usd>={MinPrice:F2}");
            
        if (MaxPrice.HasValue)
            queryParts.Add($"usd<={MaxPrice:F2}");
            
        if (MinCmc.HasValue)
            queryParts.Add($"cmc>={MinCmc}");
            
        if (MaxCmc.HasValue)
            queryParts.Add($"cmc<={MaxCmc}");
            
        if (IsReserved.HasValue)
            queryParts.Add(IsReserved.Value ? "is:reserved" : "not:reserved");
        
        return string.Join(" ", queryParts);
    }
}

/// <summary>
/// Información de un set de Magic
/// </summary>
public sealed record MagicSetDto
{
    [JsonPropertyName("code")]
    public required string Code { get; init; }
    
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    
    [JsonPropertyName("released_at")]
    public DateTime ReleasedAt { get; init; }
    
    [JsonPropertyName("set_type")]
    public required string SetType { get; init; }
    
    [JsonPropertyName("card_count")]
    public int CardCount { get; init; }
    
    [JsonPropertyName("icon_svg_uri")]
    public string? IconSvgUri { get; init; }
    
    [JsonPropertyName("search_uri")]
    public string? SearchUri { get; init; }
} 