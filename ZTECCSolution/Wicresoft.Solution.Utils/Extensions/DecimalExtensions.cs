
namespace Wicresoft.Solution.Utils
{
    public static class DecimalExtensions
    {
        public static decimal Round(this decimal? d, int decimals)
        {
            if (d.HasValue)
            {
                return decimal.Round(d.Value, decimals);
            }
            return 0.00m;
        }
        public static decimal Round(this decimal d, int decimals)
        {
            return decimal.Round(d, decimals);
        }
    }
}
