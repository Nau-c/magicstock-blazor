using MagicStock.Shared.DTOs;

namespace MagicStock.Client.Services;

// Servicios básicos simplificados para que compile
public interface IDashboardService
{
    Task<StockStatsDto> GetStockStatsAsync();
    Task<IEnumerable<MagicCardDto>> GetTopValueCardsAsync(int count = 10);
}

public interface IFilterService { }
public interface IScryfallApiService { }
public interface ICardService { }
public interface IStockService { }
public interface IThemeService { Task InitializeAsync(); }
public interface IAnalyticsService { }
public interface INotificationService { }
public interface ICacheService { Task InitializeAsync(); }
public interface IChartService { }
public interface IPwaService { Task InitializeAsync(); }

public class SimpleDashboardService : IDashboardService
{
    public Task<StockStatsDto> GetStockStatsAsync()
    {
        return Task.FromResult(new StockStatsDto
        {
            TotalCards = 1234,
            UniqueCards = 567,
            TotalValue = 2457.89m,
            AverageCardValue = 1.99m,
            RarityDistribution = new Dictionary<string, int>
            {
                { "Common", 800 },
                { "Uncommon", 300 },
                { "Rare", 120 },
                { "Mythic", 14 }
            },
            ColorDistribution = new Dictionary<string, int>
            {
                { "White", 250 },
                { "Blue", 200 },
                { "Black", 180 },
                { "Red", 220 },
                { "Green", 190 }
            },
            LastUpdated = DateTime.Now
        });
    }

    public Task<IEnumerable<MagicCardDto>> GetTopValueCardsAsync(int count = 10)
    {
        var cards = new List<MagicCardDto>
        {
            new MagicCardDto
            {
                Id = "1",
                Name = "Black Lotus",
                SetCode = "LEA",
                SetName = "Limited Edition Alpha",
                Rarity = "rare",
                Prices = new CardPricesDto { Usd = 50000m },
                TypeLine = "Artifact",
                StockQuantity = 1,
                ImageUris = new CardImageUrisDto
                {
                    Normal = "https://cards.scryfall.io/normal/front/b/d/bd8fa327-dd41-4737-8f19-2cf5eb1f7cdd.jpg"
                }
            }
        };
        
        return Task.FromResult<IEnumerable<MagicCardDto>>(cards);
    }
}

// Implementaciones básicas
public class SimpleFilterService : IFilterService { }
public class SimpleScryfallApiService : IScryfallApiService { }
public class SimpleCardService : ICardService { }
public class SimpleStockService : IStockService { }
public class SimpleThemeService : IThemeService { public Task InitializeAsync() => Task.CompletedTask; }
public class SimpleAnalyticsService : IAnalyticsService { }
public class SimpleNotificationService : INotificationService { }
public class SimpleCacheService : ICacheService { public Task InitializeAsync() => Task.CompletedTask; }
public class SimpleChartService : IChartService { }
public class SimplePwaService : IPwaService { public Task InitializeAsync() => Task.CompletedTask; } 