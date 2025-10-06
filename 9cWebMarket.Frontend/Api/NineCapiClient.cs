using Microsoft.Extensions.Options;
using NineCWebMarket.Frontend.Configuration;

namespace NineCWebMarket.Frontend.Api;

public class NineCapiClient
{
    private readonly IOptions<NineChroniclesEndpoints> _configuration;
    private readonly IHttpClientFactory _httpClientFactory;


    public NineCapiClient(IOptions<NineChroniclesEndpoints> configuration, IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public HttpClient GetClient(string planet)
    {
        if (!_configuration.Value.NineCapi.TryGetValue(planet, out var endpoint))
        {
            throw new ArgumentException($"Invalid planet:{planet}");
        }

        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(endpoint);

        return client;
    }
}