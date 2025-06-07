using MagicStock.Core.Entities;
using MagicStock.Core.ValueObjects;

namespace MagicStock.Core.Interfaces;

/// <summary>
/// Servicio para interactuar con la API de Scryfall de manera profesional
/// Implementa cache, retry policies y rate limiting
/// </summary>
public interface IScryfallApiService
{
    /// <summary>
    /// Busca cartas por nombre con paginación y cache inteligente
    /// </summary>
    Task<ApiResponse<PaginatedResult<Card>>> SearchCardsAsync(
        string query, 
        int page = 1, 
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Obtiene una carta específica por ID de Scryfall
    /// </summary>
    Task<ApiResponse<Card>> GetCardByIdAsync(
        string cardId, 
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Obtiene cartas por filtros avanzados
    /// </summary>
    Task<ApiResponse<PaginatedResult<Card>>> GetCardsByFiltersAsync(
        CardSearchFilters filters, 
        int page = 1, 
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Obtiene todas las cartas de un set específico
    /// </summary>
    Task<ApiResponse<PaginatedResult<Card>>> GetCardsBySetAsync(
        string setCode, 
        int page = 1, 
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Obtiene cartas aleatorias para discovery
    /// </summary>
    Task<ApiResponse<IEnumerable<Card>>> GetRandomCardsAsync(
        int count = 10, 
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Busca cartas con autocomplete para el buscador
    /// </summary>
    Task<ApiResponse<IEnumerable<string>>> GetCardNameSuggestionsAsync(
        string partialName, 
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Obtiene información de un set por código
    /// </summary>
    Task<ApiResponse<MagicSet>> GetSetByCodeAsync(
        string setCode, 
        CancellationToken cancellationToken = default);
} 