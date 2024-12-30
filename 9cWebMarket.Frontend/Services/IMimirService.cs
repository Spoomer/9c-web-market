using Nekoyume.Model.Item;
using NineCWebMarket.Frontend.Api;
using NineCWebMarket.Frontend.Models.Mimir;

namespace NineCWebMarket.Frontend.Services;

public interface IMimirService
{
    Task<IReadOnlyList<ProductItem>> GetItemProductsAsync(string planet, int skip, ItemSubType itemSubType, ProductSortBy sortBy, SortDirection sortDirection);
}