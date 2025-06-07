using MagicStock.Core.Entities;

namespace MagicStock.Core.Interfaces;

public interface ICardRepository
{
    Task<IEnumerable<Card>> GetAllCardsAsync(CancellationToken cancellationToken = default);
    
    Task<Card?> GetCardByIdAsync(string id, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Card>> SearchCardsAsync(string searchTerm, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Card>> GetCardsByFiltersAsync(
        string? name = null,
        string? setCode = null,
        string? rarity = null,
        string[]? colors = null,
        string? type = null,
        CancellationToken cancellationToken = default);
    
    Task<bool> AddCardToStockAsync(Card card, CancellationToken cancellationToken = default);
    
    Task<bool> UpdateCardStockAsync(string cardId, int quantity, CancellationToken cancellationToken = default);
    
    Task<bool> RemoveCardFromStockAsync(string cardId, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Card>> GetStockCardsAsync(CancellationToken cancellationToken = default);
} 