using System;
using System.Net;
using System.Web.Mvc;

namespace FI.WebAtividadeEntrevista.App_Start
{
    public class AppExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null || filterContext.ExceptionHandled) return;

            var ex = filterContext.Exception;
            var http = filterContext.HttpContext;

            var status = ex is ArgumentException
                ? HttpStatusCode.BadRequest
                : HttpStatusCode.InternalServerError;

            var message = ex is ArgumentException
                ? ex.Message
                : "Ocorreu um erro inesperado. Tente novamente.";

            if (http.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult
                {
                    Data = new { success = false, message },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                http.Response.StatusCode = (int)status;
                http.Response.TrySkipIisCustomErrors = true;
                filterContext.ExceptionHandled = true;
                return;
            }

            
        }
    }
}