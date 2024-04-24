using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Identity.API.Filters
{
    public class ResourceFilter : Attribute, IResourceFilter
    {
        private Stopwatch _stopwatch;

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _stopwatch = Stopwatch.StartNew();
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _stopwatch.Stop();

            var actionName = context.ActionDescriptor.DisplayName;
            var executionTime = _stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"\n\n\n\n\nAction '{actionName}' executed in {executionTime} ms\n\n\n\n\n");
        }
    }
}
