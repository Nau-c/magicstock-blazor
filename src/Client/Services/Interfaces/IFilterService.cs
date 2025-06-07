using MagicStock.Shared.Models;

namespace MagicStock.Client.Services.Interfaces;

/// <summary>
/// Servicio para gestión avanzada de filtros combinables
/// Implementa filtros dinámicos, guardado de presets y búsqueda inteligente
/// </summary>
public interface IFilterService
{
    /// <summary>
    /// Aplica filtros combinables a una colección de cartas
    /// </summary>
    Task<IEnumerable<MagicCardDto>> ApplyFiltersAsync(
        IEnumerable<MagicCardDto> cards, 
        AdvancedFiltersDto filters);
    
    /// <summary>
    /// Obtiene sugerencias de filtros basadas en el stock actual
    /// </summary>
    Task<FilterSuggestionsDto> GetFilterSuggestionsAsync();
    
    /// <summary>
    /// Guarda un preset de filtros
    /// </summary>
    Task SaveFilterPresetAsync(string name, AdvancedFiltersDto filters);
    
    /// <summary>
    /// Obtiene todos los presets guardados
    /// </summary>
    Task<IEnumerable<FilterPresetDto>> GetFilterPresetsAsync();
    
    /// <summary>
    /// Elimina un preset de filtros
    /// </summary>
    Task DeleteFilterPresetAsync(string presetId);
    
    /// <summary>
    /// Busca cartas con búsqueda inteligente (fuzzy search)
    /// </summary>
    Task<IEnumerable<MagicCardDto>> SmartSearchAsync(
        IEnumerable<MagicCardDto> cards, 
        string searchTerm);
    
    /// <summary>
    /// Obtiene filtros populares/recomendados
    /// </summary>
    Task<IEnumerable<FilterPresetDto>> GetPopularFiltersAsync();
    
    /// <summary>
    /// Valida si los filtros son válidos
    /// </summary>
    Task<FilterValidationResult> ValidateFiltersAsync(AdvancedFiltersDto filters);
}

/// <summary>
/// DTO para filtros avanzados combinables
/// </summary>
public sealed record AdvancedFiltersDto
{
    // Filtros básicos
    public string? Name { get; init; }
    public string? SetCode { get; init; }
    public string[]? Rarities { get; init; }
    public string[]? Colors { get; init; }
    public string[]? ColorIdentities { get; init; }
    public string[]? Types { get; init; }
    public string? Artist { get; init; }
    
    // Filtros de precio
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
    public PriceComparisonType PriceComparison { get; init; } = PriceComparisonType.Current;
    
    // Filtros de CMC
    public int? MinCmc { get; init; }
    public int? MaxCmc { get; init; }
    public CmcComparisonType CmcComparison { get; init; } = CmcComparisonType.Exact;
    
    // Filtros de stock
    public bool? OnlyInStock { get; init; }
    public int? MinStockQuantity { get; init; }
    public int? MaxStockQuantity { get; init; }
    public string[]? Conditions { get; init; }
    
    // Filtros de fecha
    public DateTime? ReleasedAfter { get; init; }
    public DateTime? ReleasedBefore { get; init; }
    public DateTime? AddedAfter { get; init; }
    public DateTime? AddedBefore { get; init; }
    
    // Filtros especiales
    public bool? IsReserved { get; init; }
    public bool? HasFoil { get; init; }
    public bool? IsLegal { get; init; }
    public string[]? Formats { get; init; }
    
    // Filtros de texto
    public string? OracleTextContains { get; init; }
    public string? FlavorTextContains { get; init; }
    public bool? HasFlavorText { get; init; }
    
    // Configuración de búsqueda
    public SearchMode SearchMode { get; init; } = SearchMode.And;
    public SortOption SortBy { get; init; } = SortOption.Name;
    public SortDirection SortDirection { get; init; } = SortDirection.Ascending;
    public bool IncludeFuzzySearch { get; init; } = true;
}

/// <summary>
/// Sugerencias de filtros basadas en el stock
/// </summary>
public sealed record FilterSuggestionsDto
{
    public required string[] AvailableSets { get; init; }
    public required string[] AvailableRarities { get; init; }
    public required string[] AvailableColors { get; init; }
    public required string[] AvailableTypes { get; init; }
    public required string[] AvailableArtists { get; init; }
    public required string[] AvailableFormats { get; init; }
    public required PriceRangeDto PriceRange { get; init; }
    public required CmcRangeDto CmcRange { get; init; }
    public required DateRangeDto ReleaseDateRange { get; init; }
}

/// <summary>
/// Preset de filtros guardado
/// </summary>
public sealed record FilterPresetDto
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required AdvancedFiltersDto Filters { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime LastUsed { get; init; }
    public required int UsageCount { get; init; }
    public required bool IsPublic { get; init; }
    public required string[]? Tags { get; init; }
}

/// <summary>
/// Resultado de validación de filtros
/// </summary>
public sealed record FilterValidationResult
{
    public required bool IsValid { get; init; }
    public required string[] Errors { get; init; }
    public required string[] Warnings { get; init; }
    public required int EstimatedResultCount { get; init; }
}

/// <summary>
/// Rangos de datos para sugerencias
/// </summary>
public sealed record PriceRangeDto(decimal Min, decimal Max);
public sealed record CmcRangeDto(int Min, int Max);
public sealed record DateRangeDto(DateTime Min, DateTime Max);

/// <summary>
/// Enums para configuración de filtros
/// </summary>
public enum PriceComparisonType
{
    Current,
    Acquisition,
    Difference
}

public enum CmcComparisonType
{
    Exact,
    Range,
    EvenOdd
}

public enum SearchMode
{
    And,
    Or,
    Exact
}

public enum SortOption
{
    Name,
    Price,
    Cmc,
    ReleaseDate,
    DateAdded,
    Rarity,
    Set,
    StockQuantity,
    TotalValue
}

public enum SortDirection
{
    Ascending,
    Descending
} 