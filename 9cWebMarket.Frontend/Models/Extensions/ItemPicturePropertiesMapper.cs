using NineCWebMarket.Frontend.Models.Api;

namespace NineCWebMarket.Frontend.Models.Extensions;

public static class ItemPicturePropertiesMapper
{
    public static ItemPictureProperties ToItemPictureProperties(this MarketTransaction marketTransaction)
    {
        return new ItemPictureProperties
        {
            Ticker = marketTransaction.Ticker,
            ItemId = marketTransaction.ItemId,
            Grade = marketTransaction.TradeItem.Grade,
            Level = marketTransaction.TradeItem.Level,
            Quantity = marketTransaction.TradeItem.Quantity,
            ItemIdOptions = marketTransaction.ItemIdOptions,
            ItemType = marketTransaction.TradeItem.ItemType,
            SkillModels = marketTransaction.TradeItem.SkillModels ?? [],
            StatModels = marketTransaction.TradeItem.StatModels ?? []
        };
    }

    public static ItemPictureProperties? ToItemPictureProperties(this IGetItemProducts_Products_Items_Object_TradableItem tradableItem)
    {
        if (tradableItem is IArmor armor)
        {
            return armor.ToItemPictureProperties();
        }

        return null;
    }

    public static ItemPictureProperties ToItemPictureProperties(this IArmor armor)
    {
        return new ItemPictureProperties
        {
            ItemId = armor.Id,
            Grade = armor.Grade,
            Level = armor.Level,
            Quantity = 1,
            ItemIdOptions = $"{armor.Id}_{armor.StatsMap.Value.Count}+{armor.Skills.Count}",
            ItemType = (int)armor.ItemType,
            SkillModels = armor.Skills.ToSkillModels(),
            StatModels = armor.StatsMap.ToStatModels()
        };
    }

    private static IReadOnlyList<StatModel> ToStatModels(this IGetItemProducts_Products_Items_Object_TradableItem_StatsMap statsMap)
    {
        List<StatModel> stats = [];
        foreach (var stat in statsMap.Value)
        {
            stats.Add(new StatModel((int)stat.Value.StatType, stat.Value.BaseValue, stat.Value.AdditionalValue));
        }

        return stats;
    }

    private static IReadOnlyList<SkillModel> ToSkillModels(this IReadOnlyList<IGetItemProducts_Products_Items_Object_TradableItem_Skills> skillsMap)
    {
        List<SkillModel> skills = [];
        foreach (var skill in skillsMap)
        {
            skills.Add( new SkillModel(skill.SkillRow.Id, skill.StatPowerRatio, skill.SkillRow.HitCount, skill.Chance, (int)skill.SkillRow.ElementalType,
                (int)skill.ReferencedStatType, skill.SkillRow.Cooldown, skill.Power, (int)skill.SkillRow.SkillCategory));
        }

        return skills;
    }
    
}