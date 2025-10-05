// ReSharper disable InconsistentNaming
namespace NineCWebMarket.Frontend.Api;

public class NineCapiEnums
{
    public enum MarketOrderType
    {
        cp,
        cp_desc,
        price,
        price_desc,
        grade,
        grade_desc,
        crystal,
        crystal_desc,
        crystal_per_price,
        crystal_per_price_desc,
        level,
        level_desc,
        opt_count,
        opt_count_desc,
        unit_price,
        unit_price_desc
    }


    public static MarketOrderType FromMimirEnums(ProductSortBy productSortBy, SortDirection sortDirection)
    {
        return productSortBy switch
        {
            ProductSortBy.CP => sortDirection switch
            {
                SortDirection.ASCENDING => MarketOrderType.cp,
                SortDirection.DESCENDING => MarketOrderType.cp_desc,
                _ => throw new ArgumentOutOfRangeException(nameof(sortDirection), sortDirection, null)
            },
            ProductSortBy.CRYSTAL => sortDirection switch
            {
                SortDirection.ASCENDING => MarketOrderType.crystal,
                SortDirection.DESCENDING => MarketOrderType.crystal_desc,
                _ => throw new ArgumentOutOfRangeException(nameof(sortDirection), sortDirection, null)
            },
            ProductSortBy.GRADE => sortDirection switch
            {
                SortDirection.ASCENDING => MarketOrderType.grade,
                SortDirection.DESCENDING => MarketOrderType.grade_desc,
                _ => throw new ArgumentOutOfRangeException(nameof(sortDirection), sortDirection, null)
            },
            ProductSortBy.PRICE => sortDirection switch
            {
                SortDirection.ASCENDING => MarketOrderType.price,
                SortDirection.DESCENDING => MarketOrderType.price_desc,
                _ => throw new ArgumentOutOfRangeException(nameof(sortDirection), sortDirection, null)
            },
            ProductSortBy.UNIT_PRICE => sortDirection switch
            {
                SortDirection.ASCENDING => MarketOrderType.unit_price,
                SortDirection.DESCENDING => MarketOrderType.unit_price_desc,
                _ => throw new ArgumentOutOfRangeException(nameof(sortDirection), sortDirection, null)
            },
            ProductSortBy.CRYSTAL_PER_PRICE => sortDirection switch
            {
                SortDirection.ASCENDING => MarketOrderType.crystal_per_price,
                SortDirection.DESCENDING => MarketOrderType.crystal_per_price_desc,
                _ => throw new ArgumentOutOfRangeException(nameof(sortDirection), sortDirection, null)
            },
            _ => throw new ArgumentOutOfRangeException(nameof(productSortBy), productSortBy, null)
        };
    }
}