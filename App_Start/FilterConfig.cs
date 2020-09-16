using System.Web;
using System.Web.Mvc;

namespace Kazan_Session3_API_16_9
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
