using NineCWebMarket.Frontend.Models.Api;

namespace NineCWebMarket.Frontend.Api;

public interface IApiClient
{
    Task<IReadOnlyCollection<string>> GetPlanetsNamesAsync();
    Task<MarketHistory?> GetMarketHistoryAsync(string planet, string? before = null);
}