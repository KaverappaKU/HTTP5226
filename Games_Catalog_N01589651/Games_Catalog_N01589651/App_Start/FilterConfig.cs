using System.Web;
using System.Web.Mvc;

namespace Games_Catalog_N01589651
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
