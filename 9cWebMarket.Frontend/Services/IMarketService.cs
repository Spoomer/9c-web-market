using Nekoyume.Model.Item;
using NineCWebMarket.Frontend.Api;
using NineCWebMarket.Frontend.Models.Mimir;

namespace NineCWebMarket.Frontend.Services;

public interface IMarketService
{
    Task<IReadOnlyList<ProductItem>> GetItemProductsAsync(string planet, int skip, int take, ItemSubType itemSubType,
        ProductSortBy sortBy, SortDirection sortDirection = SortDirection.ASCENDING, string avatarAddress = "");
}