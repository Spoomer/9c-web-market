using System.Text.Json.Serialization;

namespace NineCWebMarket.Frontend.Models.Api;

public record TradeItem(
    [property: JsonPropertyName("exist")] bool Exist,
    [property: JsonPropertyName("unitPrice")]
    decimal UnitPrice,
    [property: JsonPropertyName("ticker")] string Ticker,
    [property: JsonPropertyName("quantity")]
    int Quantity,
    [property: JsonPropertyName("legacy")] bool Legacy,
    [property: JsonPropertyName("decimalPlaces")]
    int DecimalPlaces,
    [property: JsonPropertyName("productId")]
    string ProductId,
    [property: JsonPropertyName("sellerAvatarAddress")]
    string SellerAvatarAddress,
    [property: JsonPropertyName("price")] int Price,
    [property: JsonPropertyName("sellerAgentAddress")]
    string SellerAgentAddress,
    [property: JsonPropertyName("registeredBlockIndex")]
    int RegisteredBlockIndex,
    [property: JsonPropertyName("itemType")]
    int? ItemType,
    [property: JsonPropertyName("level")] int? Level,
    [property: JsonPropertyName("crystal")]
    int? Crystal,
    [property: JsonPropertyName("optionCountFromCombination")]
    int? OptionCountFromCombination,
    [property: JsonPropertyName("crystalPerPrice")]
    int? CrystalPerPrice,
    [property: JsonPropertyName("tradableId")]
    string TradableId,
    [property: JsonPropertyName("itemSubType")]
    int? ItemSubType,
    [property: JsonPropertyName("skillModels")]
    IReadOnlyList<SkillModel>? SkillModels,
    [property: JsonPropertyName("statModels")]
    IReadOnlyList<StatModel>? StatModels,
    [property: JsonPropertyName("itemId")] int? ItemId,
    [property: JsonPropertyName("combatPoint")]
    int? CombatPoint,
    [property: JsonPropertyName("grade")] int? Grade,
    [property: JsonPropertyName("elementalType")]
    int? ElementalType,
    [property: JsonPropertyName("setId")] int? SetId
);