using System.Text.Json.Serialization;

namespace NineCWebMarket.Frontend.Models.Api;

public record FungibleAssetValueProduct(
    [property: JsonPropertyName("productId")] string ProductId,
    [property: JsonPropertyName("sellerAgentAddress")] string SellerAgentAddress,
    [property: JsonPropertyName("sellerAvatarAddress")] string SellerAvatarAddress,
    [property: JsonPropertyName("price")] int Price,
    [property: JsonPropertyName("quantity")] int Quantity,
    [property: JsonPropertyName("registeredBlockIndex")] int RegisteredBlockIndex,
    [property: JsonPropertyName("exist")] bool Exist,
    [property: JsonPropertyName("legacy")] bool Legacy,
    [property: JsonPropertyName("unitPrice")] double UnitPrice,
    [property: JsonPropertyName("decimalPlaces")] int DecimalPlaces,
    [property: JsonPropertyName("ticker")] string Ticker
);
