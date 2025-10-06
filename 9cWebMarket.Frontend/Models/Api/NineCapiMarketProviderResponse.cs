using System.Text.Json.Serialization;

namespace NineCWebMarket.Frontend.Models.Api;

public record NineCapiMarketProviderResponse(
    [property: JsonPropertyName("totalCount")]
    int TotalCount,
    [property: JsonPropertyName("limit")] int Limit,
    [property: JsonPropertyName("offset")] int Offset,
    [property: JsonPropertyName("itemProducts")]
    IReadOnlyList<TradeItem> ItemProducts,
    [property: JsonPropertyName("fungibleAssetValueProducts")]
    IReadOnlyList<FungibleAssetValueProduct> FungibleAssetValueProducts
)
{
    public static NineCapiMarketProviderResponse Empty { get; } = new(0, 0, 0, [], []);
};