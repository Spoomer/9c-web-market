using Nekoyume.Model.Elemental;
using Nekoyume.Model.Skill;
using Nekoyume.Model.Stat;
using Nekoyume.TableData;

namespace NineCWebMarket.Frontend.Models.Mimir;

public record Skill
{
    public SkillSheet.Row SkillRow { get; set; }
    public long Power { get; set; }
    public int Chance { get; set; }
    public int StatPowerRatio { get; set; }
    public StatType ReferencedStatType { get; set; }
}

[Serializable]
public class Row : SheetRow<int>
{
    public override int Key => this.Id;

    public int Id { get; set; }

    public ElementalType ElementalType { get; set; }

    public SkillType? SkillType { get; set; }

    public SkillCategory SkillCategory { get; set; }

    public SkillTargetType SkillTargetType { get; set; }

    public int HitCount { get; set; }

    public int Cooldown { get; set; }

    public bool Combo { get; set; }

    public override void Set(IReadOnlyList<string> fields)
    {
        this.Id = TableExtensions.ParseInt(fields[0]);
        this.ElementalType = (ElementalType) Enum.Parse(typeof(ElementalType), fields[1]);
        this.SkillType = (SkillType) Enum.Parse(typeof(SkillType), fields[2]);
        this.SkillCategory = (SkillCategory) Enum.Parse(typeof(SkillCategory), fields[3]);
        this.SkillTargetType = (SkillTargetType) Enum.Parse(typeof(SkillTargetType), fields[4]);
        this.HitCount = TableExtensions.ParseInt(fields[5]);
        this.Cooldown = TableExtensions.ParseInt(fields[6]);
        this.Combo = fields.Count > 7 && TableExtensions.ParseBool(fields[7], false);
    }
}