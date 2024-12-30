using Nekoyume.Model.Stat;

namespace NineCWebMarket.Frontend.Models.Mimir.Stats;

/// <summary>
/// <see cref="Nekoyume.Model.Stat.DecimalStat"/>.
/// </summary>

public record DecimalStat
{
    public StatType StatType { get; set; }
    public decimal BaseValue { get; set; }
    public decimal AdditionalValue { get; set; }
    
}
