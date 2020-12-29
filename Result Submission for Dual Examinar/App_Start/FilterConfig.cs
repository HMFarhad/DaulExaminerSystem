using System.Web;
using System.Web.Mvc;

namespace Result_Submission_for_Dual_Examinar
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
