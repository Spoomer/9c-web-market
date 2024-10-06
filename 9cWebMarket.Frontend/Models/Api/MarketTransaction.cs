using System.Text.Json.Serialization;

namespace NineCWebMarket.Frontend.Models.Api;

public record MarketTransaction(
    [property: JsonPropertyName("blockTime")] DateTime BlockTime,
    [property: JsonPropertyName("txId")] string? TxId,
    [property: JsonPropertyName("ticker")] string? Ticker,
    [property: JsonPropertyName("from")] string From,
    [property: JsonPropertyName("fromAvatar")] string FromAvatar,
    [property: JsonPropertyName("item")] TradeItem TradeItem,
    [property: JsonPropertyName("blockIndex")] int BlockIndex,
    [property: JsonPropertyName("to")] string To,
    [property: JsonPropertyName("toAvatar")] string ToAvatar,
    [property: JsonPropertyName("type")] string? Type,
    [property: JsonPropertyName("itemId_options")] string ItemIdOptions,
    [property: JsonPropertyName("itemSubType_grade_level")] string ItemSubTypeGradeLevel,
    [property: JsonPropertyName("itemSubType_grade")] string ItemSubTypeGrade,
    [property: JsonPropertyName("itemId")] int? ItemId,
    [property: JsonPropertyName("itemId_level_options")] string ItemIdLevelOptions,
    [property: JsonPropertyName("itemId_level")] string ItemIdLevel,
    [property: JsonPropertyName("itemSubType_grade_level_options")] string ItemSubTypeGradeLevelOptions,
    [property: JsonPropertyName("itemSubType")] int? ItemSubType,
    [property: JsonPropertyName("itemSubType_grade_options")] string ItemSubTypeGradeOptions
);