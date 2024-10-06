using System.Text.Json.Serialization;

namespace NineCWebMarket.Frontend.Models.Api;

public record SkillModel(
    [property: JsonPropertyName("skillId")] int SkillId,
    [property: JsonPropertyName("statPowerRatio")] int StatPowerRatio,
    [property: JsonPropertyName("hitCount")] int HitCount,
    [property: JsonPropertyName("chance")] int Chance,
    [property: JsonPropertyName("elementalType")] int ElementalType,
    [property: JsonPropertyName("referencedStatType")] int ReferencedStatType,
    [property: JsonPropertyName("cooldown")] int Cooldown,
    [property: JsonPropertyName("power")] int Power,
    [property: JsonPropertyName("skillCategory")] int SkillCategory
);