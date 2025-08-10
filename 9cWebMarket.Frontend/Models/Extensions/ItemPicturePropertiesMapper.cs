using System.Text.Json;
using Nekoyume.Model.Elemental;
using Nekoyume.Model.Item;
using Nekoyume.Model.Skill;
using Nekoyume.Model.Stat;
using Nekoyume.TableData;
using NineCWebMarket.Frontend.Models.Api;
using NineCWebMarket.Frontend.Models.Mimir;
using Skill = NineCWebMarket.Frontend.Models.Mimir.Skill;
using StatMap = NineCWebMarket.Frontend.Models.Mimir.Stats.StatMap;

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
            ItemType = (int) equipment.ItemType,
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

    public static List<StatModel> ToStatModels(this StatMap statsMap)
    {
        List<StatModel> stats = [];
        foreach (var stat in statsMap.Value)
        {
            stats.Add(new StatModel((int) stat.Value.StatType, stat.Value.BaseValue, false));
            stats.Add(new StatModel((int) stat.Value.StatType, stat.Value.AdditionalValue, true));
        }

        return stats;
    }

    public static StatMap ToStatMap(this IEnumerable<StatModel> statsModels)
    {
        var statsMap = new StatMap();
        foreach (var stat in statsModels.GroupBy(x => x.Type))
        {
            decimal baseValue = 0;
            decimal additionalValue = 0;
            foreach (var statModel in stat)
            {
                if (statModel.Additional)
                {
                    additionalValue = statModel.Value;
                }
                else
                {
                    baseValue = statModel.Value;
                }
            }

            var statType = (StatType) stat.Key;
            statsMap.Value[statType] = new NineCWebMarket.Frontend.Models.Mimir.Stats.DecimalStat
            {
                StatType = statType,
                BaseValue = baseValue,
                AdditionalValue = additionalValue
            };
        }

        return statsMap;
    }

    public static List<SkillModel> ToSkillModels(this IEnumerable<Skill> skillsMap)
    {
        List<SkillModel> skills = [];
        foreach (var skill in skillsMap)
        {
            skills.Add(new SkillModel(skill.SkillRow.Id, skill.StatPowerRatio, skill.SkillRow.HitCount, skill.Chance,
                (int) skill.SkillRow.ElementalType,
                (int) skill.ReferencedStatType, skill.SkillRow.Cooldown, skill.Power,
                (int) skill.SkillRow.SkillCategory));
        }

        return skills;
    }

    public static List<Skill> ToSkills(this IEnumerable<SkillModel> skillsModels)
    {
        List<Skill> skills = [];
        foreach (var skillModel in skillsModels)
        {
            var row = new Row
            {
                Id = skillModel.SkillId,
                ElementalType = (ElementalType) skillModel.ElementalType,
                SkillCategory = (SkillCategory) skillModel.SkillCategory,
                SkillTargetType = SkillTargetType.Enemy,
                HitCount = skillModel.HitCount,
                Cooldown = skillModel.Cooldown,
                Combo = false
            };
            skills.Add(new Skill
            {
                SkillRow = JsonSerializer.Deserialize<SkillSheet.Row>(JsonSerializer.Serialize(row)),
                Power = skillModel.Power,
                Chance = skillModel.Chance,
                StatPowerRatio = skillModel.StatPowerRatio,
                ReferencedStatType = (StatType) skillModel.ReferencedStatType
            });
        }

        return skills;
    }

    public static List<ProductItem> ToProductItems(this IEnumerable<TradeItem> responseItemProducts)
    {
        var result = new List<ProductItem>();
        foreach (var item in responseItemProducts)
        {
            var equipment = new Equipment((ElementalType) item.ElementalType, item.Grade ?? 0, item.ItemId ?? 0,
                (ItemSubType) (item.ItemSubType ?? 0), (ItemType) (item.ItemType ?? 0),
                false, 0, item.ItemId?.ToString() ?? "", item.Level ?? 0, false,
                item.OptionCountFromCombination ?? 0, item.RegisteredBlockIndex, item.SetId ?? 0,
                string.Empty, item.SkillModels?.ToSkills() ?? [], item.StatModels?.ToStatMap() ?? new StatMap());
            
            var product = new Product(item.Quantity, item.ProductId, "NON_FUNGIBLE",
                item.RegisteredBlockIndex, item.SellerAgentAddress, item.SellerAvatarAddress, equipment,
                new Price(0, item.Price.ToString(), item.Price.ToString(), "NCG"));
            
            var productItem = new ProductItem(item.SellerAvatarAddress, item.CombatPoint, item.Crystal,
                item.CrystalPerPrice, "", (int) item.UnitPrice, product);
            result.Add(productItem);
        }

        return result;
    }
}