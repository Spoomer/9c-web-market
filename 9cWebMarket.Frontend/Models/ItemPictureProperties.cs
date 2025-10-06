using NineCWebMarket.Frontend.Models.Api;

namespace NineCWebMarket.Frontend.Models;

public class ItemPictureProperties
{
    public string? Ticker { get; set; }
    public int? ItemId { get; set;}
    public int? Grade { get; set;}
    public int? Level { get; set;}
    public decimal? Quantity { get; set;}
    public string? ItemIdOptions { get; set;}
    public int? ItemType { get; set;}
    public IReadOnlyList<SkillModel> SkillModels { get; set; } = [];
    public IReadOnlyList<StatModel> StatModels { get; set; } = [];
    public int OptionCount { get; set; }
}