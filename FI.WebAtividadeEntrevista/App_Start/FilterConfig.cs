using System.Web;
using System.Web.Mvc;
using FI.WebAtividadeEntrevista.App_Start;

namespace FI.WebAtividadeEntrevista
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AppExceptionFilterAttribute()); 
        }
    }
}
