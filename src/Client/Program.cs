using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Blazored.LocalStorage;
using Blazored.Toast;
using MagicStock.Client;
using MagicStock.Client.Services;
using MagicStock.Client.Services.Interfaces;
using Polly;
using Polly.Extensions.Http;
using System.Net;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// 🎯 Configuración de componentes raíz
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 🌐 Configuración de HttpClient con Polly para resilience
builder.Services.AddHttpClient("ScryfallApi", client =>
{
    client.BaseAddress = new Uri("https://api.scryfall.com/");
    client.DefaultRequestHeaders.Add("User-Agent", "MagicStock/1.1 (https://github.com/tu-usuario/magicstock-blazor)");
    client.Timeout = TimeSpan.FromSeconds(30);
})
.AddPolicyHandler(GetRetryPolicy())
.AddPolicyHandler(GetCircuitBreakerPolicy());

// 📦 Servicios de terceros
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = MudBlazor.Defaults.Classes.Position.BottomRight;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 5000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
});

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredToast();

// 🎨 Servicios de aplicación
builder.Services.AddScoped<IScryfallApiService, ScryfallApiService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IThemeService, ThemeService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ICacheService, CacheService>();

// 📊 Servicios para Dashboard v1.1
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IChartService, ChartService>();
builder.Services.AddScoped<IFilterService, FilterService>();

// 🔄 Cache en memoria
builder.Services.AddMemoryCache();

// 📱 Configuración PWA
builder.Services.AddScoped<IPwaService, PwaService>();

// 🔧 Configuración de logging
builder.Logging.SetMinimumLevel(LogLevel.Information);

var host = builder.Build();

// 🚀 Inicialización de servicios
await InitializeServicesAsync(host.Services);

await host.RunAsync();

// 🛡️ Políticas de resilience con Polly
static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == HttpStatusCode.TooManyRequests)
        .WaitAndRetryAsync(
            retryCount: 3,
            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) + TimeSpan.FromMilliseconds(100),
            onRetry: (outcome, timespan, retryCount, context) =>
            {
                Console.WriteLine($"🔄 Retry {retryCount} after {timespan}s delay");
            });
}

static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(
            handledEventsAllowedBeforeBreaking: 5,
            durationOfBreak: TimeSpan.FromSeconds(30),
            onBreak: (exception, duration) =>
            {
                Console.WriteLine($"🚫 Circuit breaker opened for {duration}s");
            },
            onReset: () =>
            {
                Console.WriteLine("✅ Circuit breaker reset");
            });
}

// 🎯 Inicialización de servicios críticos
static async Task InitializeServicesAsync(IServiceProvider services)
{
    try
    {
        // Inicializar tema
        var themeService = services.GetRequiredService<IThemeService>();
        await themeService.InitializeAsync();

        // Inicializar cache
        var cacheService = services.GetRequiredService<ICacheService>();
        await cacheService.InitializeAsync();

        // Inicializar PWA
        var pwaService = services.GetRequiredService<IPwaService>();
        await pwaService.InitializeAsync();

        Console.WriteLine("✅ Servicios inicializados correctamente");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error inicializando servicios: {ex.Message}");
    }
} 