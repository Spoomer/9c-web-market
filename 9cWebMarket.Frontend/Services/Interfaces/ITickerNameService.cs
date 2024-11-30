namespace NineCWebMarket.Frontend.Services.Interfaces;

public interface ITickerNameService : IReadOnlyDictionary<string, string>
{
    Task InitAsync();
}