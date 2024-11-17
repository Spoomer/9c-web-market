namespace NineCWebMarket.Frontend.Configuration;

public record NineChroniclesEndpoints
{
    public const string SectionName = "NineChroniclesEndpoints";
    public string? Planets { get; init; }
    public string? ItemName { get; init; }
    public string? RuneSheet { get; init; }
    public string? Mimir { get; init; }
}