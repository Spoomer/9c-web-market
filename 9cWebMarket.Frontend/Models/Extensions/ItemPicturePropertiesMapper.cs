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

}