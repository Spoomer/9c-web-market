using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using NineCWebMarket.Frontend.Configuration;
using NineCWebMarket.Frontend.Models.Api;

namespace NineCWebMarket.Frontend.Api;

public class ApiClient : IApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<NineChroniclesEndpoints> _endpoints;
    private Dictionary<string, Planet>? _planets;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public ApiClient(IHttpClientFactory httpClientFactory, IOptions<NineChroniclesEndpoints> endpoints)
    {
        _httpClientFactory = httpClientFactory;
        _endpoints = endpoints;
    }

    public async Task<IReadOnlyCollection<string>> GetPlanetsNamesAsync()
    {
        if (_planets is null || _planets.Count == 0)
        {
            await InitPlanetsAsync();
            if (_planets is null) return [];
        }

        return _planets.Keys;
    }
    public async Task<MarketHistory?> GetMarketHistoryAsync(string planet, string? before = null)
    {
        var httpClient = await CreateMarketHistoryHttpClient(planet);
        if (httpClient is null)
        {
            return null;
        }

        var endpoint = "shopHistory";
        if (!string.IsNullOrEmpty(before))
        {
            endpoint = $"{endpoint}?before={before}";
        }

        var result = await httpClient.GetAsync(endpoint);
        if (result.IsSuccessStatusCode)
        {
            return await result.Content.ReadFromJsonAsync<MarketHistory>();
        }

        return null;
    }

    private async Task InitPlanetsAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            if (_planets != null && _planets.Count != 0)
            {
                return;
            }

            var httpClient = _httpClientFactory.CreateClient();
            // httpClient.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", _endpoints.Value.Planets);
            _planets = (await httpClient.GetFromJsonAsync<Planet[]>(_endpoints.Value.Planets))?.ToDictionary(x => x.Name);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private async Task<HttpClient?> CreateMarketHttpClient(string planetName)
    {
        var httpClient = _httpClientFactory.CreateClient();
        if (_planets is null || _planets.Count == 0)
        {
            await InitPlanetsAsync();
            if (_planets is null) return null;
        }

        if (!_planets.TryGetValue(planetName, out var planet))
        {
            return null;
        }

        if (planet.RpcEndpoints.MarketRest.Count == 0)
        {
            return null;
        }

        httpClient.BaseAddress = new Uri(planet.RpcEndpoints.MarketRest[0]);

        return httpClient;
    }

    private async Task<HttpClient?> CreateMarketHistoryHttpClient(string planetName)
    {
        var httpClient = _httpClientFactory.CreateClient();

        if (_planets is null || _planets.Count == 0)
        {
            await InitPlanetsAsync();
            if (_planets is null) return null;
        }

        if (!_planets.TryGetValue(planetName, out var planet))
        {
            return null;
        }

        if (planet.RpcEndpoints.NineCScanRest.Count == 0)
        {
            return null;
        }

        httpClient.BaseAddress = new Uri(planet.RpcEndpoints.NineCScanRest[0]);

        return httpClient;
    }
}