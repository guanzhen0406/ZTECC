using System;

namespace Wicresoft.Solution.Utils
{
    public static class DateTimeExtensions
    {
        public static int GetQuater(DateTime dateTime)
        {
            var result = (dateTime.Month-1)/3 + 1;
            return result;
        }
        public static int GetYearAndQuater(DateTime dateTime,out int quater)
        {
            int result;
            quater = GetQuater(dateTime);
            if (quater == 1)
            {
                result = dateTime.Year - 1;
                quater = 4;
            }
            else
            {
                result = dateTime.Year;
            }
            return result;
        }
        public static DateTime? TryToGetDateTime(string dateTime)
        {
            DateTime res;
            if (DateTime.TryParse(dateTime, out res))
            {
                return res;
            }
            else
            {
                return null;
            }
        }
    }
}
