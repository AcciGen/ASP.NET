using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Identity.API.Filters
{
    public class ResultFilter : Attribute, IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("\n\n\n\n\nWorking on that");
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            var user = context.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

            Console.WriteLine($"User {user} executed action: {actionName} with result: {context.Result}\n\n\n\n\n");
        }
    }
}
