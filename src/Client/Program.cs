using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Blazored.LocalStorage;
using MagicStock.Client.Services;
using MagicStock.Client.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Configuración de componentes raíz
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient básico
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// MudBlazor Services
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = MudBlazor.Defaults.Classes.Position.BottomRight;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 5000;
});

// LocalStorage
builder.Services.AddBlazoredLocalStorage();

// Servicios de aplicación
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IFilterService, FilterService>();
builder.Services.AddScoped<IScryfallApiService, ScryfallApiService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IThemeService, ThemeService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IChartService, ChartService>();
builder.Services.AddScoped<IPwaService, PwaService>();

// Cache en memoria
builder.Services.AddMemoryCache();

var host = builder.Build();

// Inicialización básica de servicios
try
{
    var themeService = host.Services.GetRequiredService<IThemeService>();
    await themeService.InitializeAsync();
    
    var cacheService = host.Services.GetRequiredService<ICacheService>();
    await cacheService.InitializeAsync();
    
    var pwaService = host.Services.GetRequiredService<IPwaService>();
    await pwaService.InitializeAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Error inicializando servicios: {ex.Message}");
}

await host.RunAsync(); 