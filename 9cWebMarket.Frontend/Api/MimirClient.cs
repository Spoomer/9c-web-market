using System.Text.Json;
using System.Text.Json.Serialization;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Microsoft.Extensions.Options;
using NineCWebMarket.Frontend.Configuration;
using NineCWebMarket.Frontend.Models.Mimir.Stats;

namespace NineCWebMarket.Frontend.Api;

public class MimirClient : IMimirClient
{
    private readonly IOptions<NineChroniclesEndpoints> _configuration;

    private readonly Dictionary<string, GraphQLHttpClient> _clientsPerPlanet = new();

    public MimirClient(IOptions<NineChroniclesEndpoints> configuration)
    {
        _configuration = configuration;
    }

    public GraphQLHttpClient GetClient(string planet)
    {
        if (_clientsPerPlanet.TryGetValue(planet, out var client))
        {
            return client;
        }

        if (!_configuration.Value.Mimir.TryGetValue(planet, out var endpoint))
        {
            throw new ArgumentException("Invalid planet");
        }
        var deserializeOptions = new JsonSerializerOptions(JsonSerializerOptions.Default);
        deserializeOptions.Converters.Add(new StatMapJsonConverter(deserializeOptions));
        deserializeOptions.Converters.Add(new JsonStringEnumConverter());

        client = new GraphQLHttpClient(endpoint, new SystemTextJsonSerializer(deserializeOptions));

        _clientsPerPlanet[planet] = client;

        return client;
    }
}