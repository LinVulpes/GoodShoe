using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GoodShoe.Controllers
{
    public class BaseAdminController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var isAdmin = HttpContext.Session.GetString("IsAdmin");
            
            if (string.IsNullOrEmpty(userEmail) || isAdmin != "true")
            {
                context.Result = RedirectToAction("Login", "Account");
                return;
            }
            
            base.OnActionExecuting(context);
        }
    }
}