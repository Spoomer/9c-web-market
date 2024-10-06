using System.Text.Json.Serialization;

namespace NineCWebMarket.Frontend.Models.Api;

public record StatModel(
    [property: JsonPropertyName("type")] int Type,
    [property: JsonPropertyName("value")] int Value,
    [property: JsonPropertyName("additional")] bool Additional
);