using System.Text.Json.Serialization;

namespace NineCWebMarket.Frontend.Models.Mimir;

public record Root(
    [property: JsonPropertyName("data")]
    GetItemProductsResponse Data
    );
public record GetItemProductsResponse(
    [property: JsonPropertyName("products")]
    Products Products
);

public record Products(
    [property: JsonPropertyName("items")] IReadOnlyList<ProductItem> Items
);

public record ProductItem(
    [property: JsonPropertyName("avatarAddress")]
    string AvatarAddress,
    [property: JsonPropertyName("combatPoint")]
    int? CombatPoint,
    [property: JsonPropertyName("crystal")]
    int? Crystal,
    [property: JsonPropertyName("crystalPerPrice")]
    int? CrystalPerPrice,
    [property: JsonPropertyName("productsStateAddress")]
    string ProductsStateAddress,
    [property: JsonPropertyName("unitPrice")]
    int? UnitPrice,
    [property: JsonPropertyName("object")] Product Object
);

public record Product(
    [property: JsonPropertyName("itemCount")]
    int ItemCount,
    [property: JsonPropertyName("productId")]
    string ProductId,
    [property: JsonPropertyName("productType")]
    string ProductType,
    [property: JsonPropertyName("registeredBlockIndex")]
    int RegisteredBlockIndex,
    [property: JsonPropertyName("sellerAgentAddress")]
    string SellerAgentAddress,
    [property: JsonPropertyName("sellerAvatarAddress")]
    string SellerAvatarAddress,
    [property: JsonPropertyName("tradableItem")]
    Equipment Equipment,
    [property: JsonPropertyName("price")] Price Price
);
public record Price(
    [property: JsonPropertyName("decimalPlaces")]
    int DecimalPlaces,
    [property: JsonPropertyName("quantity")]
    string Quantity,
    [property: JsonPropertyName("rawValue")]
    string RawValue,
    [property: JsonPropertyName("ticker")] string Ticker
);



