namespace NineCWebMarket.Frontend.Services.Interfaces;

public interface IItemNameService : IReadOnlyDictionary<int, string>
{
    Task InitAsync();
}