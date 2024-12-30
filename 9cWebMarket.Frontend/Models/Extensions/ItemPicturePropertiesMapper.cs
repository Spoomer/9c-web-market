using NineCWebMarket.Frontend.Models.Api;
using NineCWebMarket.Frontend.Models.Mimir;
using NineCWebMarket.Frontend.Models.Mimir.Stats;

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

    // public static ItemPictureProperties? ToItemPictureProperties(this Product productItem)
    // {
    //     return productItem switch
    //     {
    //         IWeapon weapon => weapon.ToItemPictureProperties(),
    //         IArmor armor => armor.ToItemPictureProperties(),
    //         IBelt belt => belt.ToItemPictureProperties(),
    //         INecklace necklace => necklace.ToItemPictureProperties(),
    //         IRing ring => ring.ToItemPictureProperties(),
    //         _ => null
    //     };
    // }
    public static ItemPictureProperties ToItemPictureProperties(this Equipment equipment)
    {
        return new ItemPictureProperties
        {
            ItemId = equipment.Id,
            Grade = equipment.Grade,
            Level = equipment.Level,
            Quantity = 1,
            ItemIdOptions = $"{equipment.Id}_{equipment.StatsMap.Value.Count}+{equipment.Skills.Count}",
            ItemType = (int)equipment.ItemType,
            SkillModels = equipment.Skills.ToSkillModels(),
            StatModels = equipment.StatsMap.ToStatModels()
        };
    }
    // public static ItemPictureProperties ToItemPictureProperties(this IWeapon weapon)
    // {
    //     return new ItemPictureProperties
    //     {
    //         ItemId = weapon.Id,
    //         Grade = weapon.Grade,
    //         Level = weapon.Level,
    //         Quantity = 1,
    //         ItemIdOptions = $"{weapon.Id}_{weapon.StatsMap.Value.Count}+{weapon.Skills.Count}",
    //         ItemType = (int)weapon.ItemType,
    //         SkillModels = weapon.Skills.ToSkillModels(),
    //         StatModels = weapon.StatsMap.ToStatModels()
    //     };
    // }
    //
    // public static ItemPictureProperties ToItemPictureProperties(this IArmor armor)
    // {
    //     return new ItemPictureProperties
    //     {
    //         ItemId = armor.Id,
    //         Grade = armor.Grade,
    //         Level = armor.Level,
    //         Quantity = 1,
    //         ItemIdOptions = $"{armor.Id}_{armor.StatsMap.Value.Count}+{armor.Skills.Count}",
    //         ItemType = (int)armor.ItemType,
    //         SkillModels = armor.Skills.ToSkillModels(),
    //         StatModels = armor.StatsMap.ToStatModels()
    //     };
    // }
    //
    // public static ItemPictureProperties ToItemPictureProperties(this IBelt belt)
    // {
    //     return new ItemPictureProperties
    //     {
    //         ItemId = belt.Id,
    //         Grade = belt.Grade,
    //         Level = belt.Level,
    //         Quantity = 1,
    //         ItemIdOptions = $"{belt.Id}_{belt.StatsMap.Value.Count}+{belt.Skills.Count}",
    //         ItemType = (int)belt.ItemType,
    //         SkillModels = belt.Skills.ToSkillModels(),
    //         StatModels = belt.StatsMap.ToStatModels()
    //     };
    // }
    //
    // public static ItemPictureProperties ToItemPictureProperties(this INecklace necklace)
    // {
    //     return new ItemPictureProperties
    //     {
    //         ItemId = necklace.Id,
    //         Grade = necklace.Grade,
    //         Level = necklace.Level,
    //         Quantity = 1,
    //         ItemIdOptions = $"{necklace.Id}_{necklace.StatsMap.Value.Count}+{necklace.Skills.Count}",
    //         ItemType = (int)necklace.ItemType,
    //         SkillModels = necklace.Skills.ToSkillModels(),
    //         StatModels = necklace.StatsMap.ToStatModels()
    //     };
    // }
    //
    // public static ItemPictureProperties ToItemPictureProperties(this IRing ring)
    // {
    //     return new ItemPictureProperties
    //     {
    //         ItemId = ring.Id,
    //         Grade = ring.Grade,
    //         Level = ring.Level,
    //         Quantity = 1,
    //         ItemIdOptions = $"{ring.Id}_{ring.StatsMap.Value.Count}+{ring.Skills.Count}",
    //         ItemType = (int)ring.ItemType,
    //         SkillModels = ring.Skills.ToSkillModels(),
    //         StatModels = ring.StatsMap.ToStatModels()
    //     };
    // }

    private static List<StatModel> ToStatModels(this StatMap statsMap)
    {
        List<StatModel> stats = [];
        foreach (var stat in statsMap.Value)
        {
            stats.Add(new StatModel((int)stat.Value.StatType, stat.Value.BaseValue, false));
            stats.Add(new StatModel((int)stat.Value.StatType, stat.Value.AdditionalValue, true));
        }
        
        return stats;
    }

    private static List<SkillModel> ToSkillModels(this IReadOnlyList<Skill> skillsMap)
    {
        List<SkillModel> skills = [];
        foreach (var skill in skillsMap)
        {
            skills.Add(new SkillModel(skill.SkillRow.Id, skill.StatPowerRatio, skill.SkillRow.HitCount, skill.Chance, (int)skill.SkillRow.ElementalType,
                (int)skill.ReferencedStatType, skill.SkillRow.Cooldown, skill.Power, (int)skill.SkillRow.SkillCategory));
        }

        return skills;
    }
}