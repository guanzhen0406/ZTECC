
namespace Wicresoft.Solution.Utils
{
    public static class DoubleExtensions
    {
        public static string Round(this double? d,int doubles=2)
        {
            if (d.HasValue)
            {
                return d.Value.ToString("N"+doubles);
            }
            return "0.00";
        }

        public static string ToPercent(this double? d, int doubles = 0)
        {
            if (d.HasValue)
            {
                return d.Value.ToString("P" + doubles);
            }
            return "";
        }

        public static string ToPercent(this double d, int doubles = 0)
        {
            return d.ToPercent(doubles);
        }
    }
}
