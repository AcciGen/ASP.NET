
namespace Identity.API.Filters
{
    public class EndpointFilter : Attribute, IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next) => await next(context);
    }

}
