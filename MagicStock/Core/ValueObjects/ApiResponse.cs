namespace MagicStock.Core.ValueObjects;

/// <summary>
/// Response wrapper para APIs externas con manejo profesional de errores
/// </summary>
public sealed record ApiResponse<T>
{
    public bool IsSuccess { get; init; }
    public T? Data { get; init; }
    public string? ErrorMessage { get; init; }
    public int? StatusCode { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    
    public static ApiResponse<T> Success(T data) => new()
    {
        IsSuccess = true,
        Data = data
    };
    
    public static ApiResponse<T> Failure(string errorMessage, int? statusCode = null) => new()
    {
        IsSuccess = false,
        ErrorMessage = errorMessage,
        StatusCode = statusCode
    };
}

/// <summary>
/// Resultado paginado genérico para APIs
/// </summary>
public sealed record PaginatedResult<T>
{
    public required IEnumerable<T> Data { get; init; }
    public required int TotalCount { get; init; }
    public required int Page { get; init; }
    public required int PageSize { get; init; }
    public required bool HasMore { get; init; }
    
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}

/// <summary>
/// Filtros de búsqueda para cartas MTG
/// </summary>
public sealed record CardSearchFilters
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
    public string? SortBy { get; init; } = "name";
    public string? SortDirection { get; init; } = "asc";
    
    /// <summary>
    /// Convierte los filtros a query string de Scryfall
    /// </summary>
    public string ToScryfallQuery()
    {
        var queryParts = new List<string>();
        
        if (!string.IsNullOrWhiteSpace(Name))
            queryParts.Add($"name:{Name}");
            
        if (!string.IsNullOrWhiteSpace(SetCode))
            queryParts.Add($"set:{SetCode}");
            
        if (!string.IsNullOrWhiteSpace(Rarity))
            queryParts.Add($"rarity:{Rarity}");
            
        if (Colors?.Any() == true)
            queryParts.Add($"color:{string.Join("", Colors)}");
            
        if (!string.IsNullOrWhiteSpace(Type))
            queryParts.Add($"type:{Type}");
            
        if (MinPrice.HasValue)
            queryParts.Add($"usd>={MinPrice}");
            
        if (MaxPrice.HasValue)
            queryParts.Add($"usd<={MaxPrice}");
            
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
/// Information de un set de Magic
/// </summary>
public sealed record MagicSet
{
    public required string Code { get; init; }
    public required string Name { get; init; }
    public required DateTime ReleasedAt { get; init; }
    public required string SetType { get; init; }
    public required int CardCount { get; init; }
    public required string? IconSvgUri { get; init; }
}

/// <summary>
/// Configuración de la aplicación
/// </summary>
public sealed record AppSettings
{
    public required ScryfallSettings Scryfall { get; init; }
    public required LocalStorageSettings LocalStorage { get; init; }
    public required UISettings UI { get; init; }
}

public sealed record ScryfallSettings
{
    public required string BaseUrl { get; init; }
    public required int RequestDelayMs { get; init; } = 100;
    public required TimeSpan CacheDuration { get; init; } = TimeSpan.FromMinutes(30);
    public required int MaxRetries { get; init; } = 3;
}

public sealed record LocalStorageSettings
{
    public required string StockKey { get; init; }
    public required string SettingsKey { get; init; }
    public required string ThemeKey { get; init; }
}

public sealed record UISettings
{
    public required int DefaultPageSize { get; init; } = 20;
    public required int DebounceDelayMs { get; init; } = 300;
    public required bool EnableAnimations { get; init; } = true;
    public required string DefaultTheme { get; init; } = "light";
} 