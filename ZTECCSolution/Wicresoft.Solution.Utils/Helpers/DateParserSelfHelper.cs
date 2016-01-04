using System;

namespace Wicresoft.Solution.Utils
{
    /// <summary>
    /// 日期转换辅助类
    /// </summary>
    public class DateParserSelfHelper
    {
        /// <summary>
        /// 获取季度编号,1表示1季度,4表示4季度
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetQuarterByDate(DateTime date)
        {
            var month = date.Month;
            return Convert.ToInt32(Math.Ceiling(month * 1.0 / 3));
        }

        /// <summary>
        /// 获取半年度编号,0表示上半年,1表示下半年
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetHalfYearByDate(DateTime date)
        {
            var month = date.Month;
            return Convert.ToInt32(Math.Ceiling(month * 1.0 / 6) - 1);
        }

        /// <summary>
        /// 获取指定日期所在月的最后一天
        /// </summary>
        /// <returns></returns>
        public static DateTime GetFirstDayOfMonth(DateTime queryTime)
        {
            var firstDay = queryTime.AddDays(-queryTime.Day + 1);
            return firstDay;
        }

        /// <summary>
        /// 获取指定日期所在月的最后一天
        /// </summary>
        /// <returns></returns>
        public static DateTime GetLastDayOfMonth(DateTime queryTime)
        {
            var lastDay = queryTime.AddMonths(1).AddDays(-queryTime.AddMonths(1).Day);
            return lastDay;
        }
    }
}
