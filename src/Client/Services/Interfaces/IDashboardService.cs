using MagicStock.Shared.Models;

namespace MagicStock.Client.Services.Interfaces;

/// <summary>
/// Servicio para generar estadísticas y datos del dashboard
/// Implementa análisis avanzado del stock con métricas profesionales
/// </summary>
public interface IDashboardService
{
    /// <summary>
    /// Obtiene estadísticas generales del stock
    /// </summary>
    Task<StockStatsDto> GetStockStatsAsync();
    
    /// <summary>
    /// Obtiene distribución de cartas por rareza
    /// </summary>
    Task<Dictionary<string, int>> GetRarityDistributionAsync();
    
    /// <summary>
    /// Obtiene distribución de valor por rareza
    /// </summary>
    Task<Dictionary<string, decimal>> GetValueByRarityAsync();
    
    /// <summary>
    /// Obtiene distribución de colores en el stock
    /// </summary>
    Task<Dictionary<string, int>> GetColorDistributionAsync();
    
    /// <summary>
    /// Obtiene distribución por sets/expansiones
    /// </summary>
    Task<Dictionary<string, int>> GetSetDistributionAsync();
    
    /// <summary>
    /// Obtiene evolución del valor del stock en el tiempo
    /// </summary>
    Task<Dictionary<DateTime, decimal>> GetValueEvolutionAsync();
    
    /// <summary>
    /// Obtiene las cartas más valiosas del stock
    /// </summary>
    Task<IEnumerable<MagicCardDto>> GetTopValueCardsAsync(int count = 10);
    
    /// <summary>
    /// Obtiene las cartas más comunes en el stock
    /// </summary>
    Task<IEnumerable<MagicCardDto>> GetMostCommonCardsAsync(int count = 10);
    
    /// <summary>
    /// Obtiene métricas de rendimiento del stock
    /// </summary>
    Task<StockPerformanceDto> GetStockPerformanceAsync();
    
    /// <summary>
    /// Obtiene recomendaciones basadas en el stock actual
    /// </summary>
    Task<IEnumerable<StockRecommendationDto>> GetRecommendationsAsync();
    
    /// <summary>
    /// Exporta estadísticas a diferentes formatos
    /// </summary>
    Task<byte[]> ExportStatsAsync(ExportFormat format);
}

/// <summary>
/// DTO para métricas de rendimiento del stock
/// </summary>
public sealed record StockPerformanceDto
{
    public required decimal TotalInvestment { get; init; }
    public required decimal CurrentValue { get; init; }
    public required decimal ProfitLoss { get; init; }
    public required decimal ProfitLossPercentage { get; init; }
    public required decimal AverageCardValue { get; init; }
    public required decimal MedianCardValue { get; init; }
    public required int TotalCards { get; init; }
    public required int UniqueCards { get; init; }
    public required DateTime LastUpdated { get; init; }
    public required Dictionary<string, decimal> PerformanceBySet { get; init; }
    public required Dictionary<string, decimal> PerformanceByRarity { get; init; }
}

/// <summary>
/// DTO para recomendaciones de stock
/// </summary>
public sealed record StockRecommendationDto
{
    public required string Type { get; init; } // "BUY", "SELL", "HOLD"
    public required string CardId { get; init; }
    public required string CardName { get; init; }
    public required string Reason { get; init; }
    public required decimal CurrentPrice { get; init; }
    public required decimal? RecommendedPrice { get; init; }
    public required int Priority { get; init; } // 1-5
    public required DateTime GeneratedAt { get; init; }
}

/// <summary>
/// Formatos de exportación disponibles
/// </summary>
public enum ExportFormat
{
    Json,
    Csv,
    Excel,
    Pdf
} 