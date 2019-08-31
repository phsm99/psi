using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace aa.Controllers
{
    public class BaseController : Controller
    {

    }


    

    public class RedirectingActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var cookie = filterContext.RequestContext.HttpContext.Request.Cookies["login"];
            var session = filterContext.HttpContext.Session["Usuario"];
            if (cookie == null || session == null || DateTime.Parse(cookie.Values["Expires"]) <= DateTime.Now)
            {
                if (cookie==null)
                {
                    filterContext.HttpContext.Session.Remove("Usuario");
                }
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Account",
                    action = "Login",
                }));
            }
        }
    }
}