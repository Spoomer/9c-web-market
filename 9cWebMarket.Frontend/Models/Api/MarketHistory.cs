using System.Text.Json.Serialization;

namespace NineCWebMarket.Frontend.Models.Api;

public record MarketHistory(
    [property: JsonPropertyName("items")] IReadOnlyList<MarketTransaction> Transactions,
    [property: JsonPropertyName("before")] string Before
);