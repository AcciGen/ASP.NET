using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Identity.API.Filters
{
    public class ActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            var user = context.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

            Console.WriteLine($"\n\n\n\n\nUser {user} is executing action: {actionName}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Action Executed\n\n\n\n\n");
        }
    }
}
