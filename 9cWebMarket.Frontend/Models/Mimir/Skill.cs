using Nekoyume.Model.Stat;
using Nekoyume.TableData;

namespace NineCWebMarket.Frontend.Models.Mimir;

/// <summary>
/// <see cref="Nekoyume.Model.Skill.Skill"/>
/// </summary>
public record Skill
{
    public SkillSheet.Row SkillRow { get; set; }
    public long Power { get; set; }
    public int Chance { get; set; }
    public int StatPowerRatio { get; set; }
    public StatType ReferencedStatType { get; set; }
}