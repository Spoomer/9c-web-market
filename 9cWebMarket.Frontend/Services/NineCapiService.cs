using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Nekoyume.Model.Item;
using NineCWebMarket.Frontend.Api;
using NineCWebMarket.Frontend.Models.Api;
using NineCWebMarket.Frontend.Models.Extensions;
using NineCWebMarket.Frontend.Models.Mimir;
using NineCWebMarket.Frontend.Models.Mimir.Stats;

namespace NineCWebMarket.Frontend.Services;

public class NineCapiService : IMarketService
{
    private readonly NineCapiClient _client;

    public NineCapiService(NineCapiClient client)
    {
        _client = client;
    }

    public async Task<IReadOnlyList<ProductItem>> GetItemProductsAsync(string planet, int skip, int take,
        ItemSubType itemSubType, ProductSortBy sortBy, SortDirection sortDirection, string avatarAddress)
    {
        if (string.IsNullOrEmpty(avatarAddress))
        {
            return [];
        }

        var httpClient = _client.GetClient(planet);
        var deserializeOptions = new JsonSerializerOptions(JsonSerializerOptions.Default);
        deserializeOptions.Converters.Add(new StatMapJsonConverter(deserializeOptions));
        deserializeOptions.Converters.Add(new JsonStringEnumConverter());

        var response = await httpClient.GetFromJsonAsync<NineCapiMarketProviderResponse>(
            $"Market/products/{avatarAddress}?offset={skip}&limit={take}",
            deserializeOptions
        ) ?? NineCapiMarketProviderResponse.Empty;

        return response.ItemProducts.ToProductItems();
    }
}