namespace SecurityMiddleWares;

public class BlockPostmanCallsMiddleware
{
    private readonly RequestDelegate _next;
    public BlockPostmanCallsMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        if (IsRequestFromPostman(context.Request))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Postman requests are not allowed.");
            return;
        }

        await _next(context);
    }

    private bool IsRequestFromPostman(HttpRequest request)
    {
        var userAgent = request.Headers["User-Agent"].ToString();
        return userAgent.Contains("Postman");
    }
}