using System.Text.Json.Serialization;

namespace NineCWebMarket.Frontend.Models.Api;

public record MarketHistory(
    [property: JsonPropertyName("items")] ICollection<MarketTransaction> Transactions,
    [property: JsonPropertyName("before")] string Before
);