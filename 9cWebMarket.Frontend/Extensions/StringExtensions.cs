namespace NineCWebMarket.Frontend.Extensions;

public static class StringExtensions
{
    public static int ToInt(this string value, int defaultValue = 0)
    {
        return int.TryParse(value, out var result) ? result : defaultValue;
    }
}