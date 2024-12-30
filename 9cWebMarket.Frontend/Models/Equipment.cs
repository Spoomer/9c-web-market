using System.Text.Json.Serialization;
using Nekoyume.Model.Elemental;
using Nekoyume.Model.Item;
using NineCWebMarket.Frontend.Models.Mimir;
using NineCWebMarket.Frontend.Models.Mimir.Stats;

namespace NineCWebMarket.Frontend.Models;

public record Equipment(
    [property: JsonPropertyName("elementalType")] ElementalType ElementalType,
    [property: JsonPropertyName("grade")] int Grade,
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("itemSubType")] ItemSubType ItemSubType,
    [property: JsonPropertyName("itemType")] ItemType ItemType,
    [property: JsonPropertyName("equipped")] bool Equipped,
    [property: JsonPropertyName("exp")] long Exp,
    [property: JsonPropertyName("itemId")] string ItemId,
    [property: JsonPropertyName("level")] int Level,
    [property: JsonPropertyName("madeWithMimisbrunnrRecipe")] bool MadeWithMimisbrunnrRecipe,
    [property: JsonPropertyName("optionCountFromCombination")] int OptionCountFromCombination,
    [property: JsonPropertyName("requiredBlockIndex")] int RequiredBlockIndex,
    [property: JsonPropertyName("setId")] int SetId,
    [property: JsonPropertyName("spineResourcePath")] string SpineResourcePath,
    [property: JsonPropertyName("skills")] IReadOnlyList<Skill> Skills,
    [property: JsonPropertyName("statsMap")] StatMap StatsMap
);