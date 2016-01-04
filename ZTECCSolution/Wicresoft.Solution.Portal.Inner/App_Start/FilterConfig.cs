using System.Web.Mvc;

namespace Wicresoft.Solution.Portal.Inner
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}