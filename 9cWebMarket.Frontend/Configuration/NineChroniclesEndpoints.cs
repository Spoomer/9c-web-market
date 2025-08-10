namespace NineCWebMarket.Frontend.Configuration;

public record NineChroniclesEndpoints
{
    public const string SectionName = "NineChroniclesEndpoints";
    public string? Planets { get; init; }
    public string? ItemName { get; init; }
    public string? RuneSheet { get; init; }
    public Dictionary<string, string> Mimir { get; set; } = new();
    public Dictionary<string, string> NineCapi { get; set; } = new();
}