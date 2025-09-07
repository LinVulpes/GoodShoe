using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GoodShoe.Controllers
{
    public class BaseAdminController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var adminId = HttpContext.Session.GetInt32("AdminId");
            var userEmail = HttpContext.Session.GetString("UserEmail");
            
            // Check if user is admin - either by AdminId OR by UserEmail
            bool isAdmin = adminId.HasValue || (!string.IsNullOrEmpty(userEmail) && IsUserAdmin(userEmail));
    
            if (!isAdmin)
            {
                context.Result = RedirectToAction("Login", "Account");
                return;
            }
            base.OnActionExecuting(context);
        }
        
        private bool IsUserAdmin(string email)
        {
            return HttpContext.Session.GetInt32("AdminId").HasValue;
        }
    }
}