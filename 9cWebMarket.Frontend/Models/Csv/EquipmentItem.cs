using CsvHelper.Configuration.Attributes;

namespace NineCWebMarket.Frontend.Models.Csv;

public class EquipmentItem
{
    [Name("id")]
    public int Id { get; set; }
    [Name("_name")]
    public required string Name { get; set; }
    [Name("item_sub_type")]
    public required string ItemSubType { get; set; }
    [Name("grade")]
    public int Grade { get; set; }
    [Name("elemental_type")]
    public required string ElementalType { get; set; }
    [Name("set_id")]
    public int SetId { get; set; }
    [Name("stat_type")]
    public required string StatType { get; set; }
    [Name("stat_value")]
    public int StatValue { get; set; }
    [Name("attack_range")]
    public int AttackRange { get; set; }

}