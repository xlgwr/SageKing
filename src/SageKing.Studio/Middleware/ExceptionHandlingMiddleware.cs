using IceRpc.Slice.Ice;
namespace SageKing.Studio.Middleware;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
            logger.LogError(ex.StackTrace);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception switch
        {
            BadHttpRequestException => StatusCodes.Status400BadRequest,
            ServerNotFoundException => StatusCodes.Status404NotFound,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

        var errorDetails = new ErrorDetails()
        {
            Error = context.Response.StatusCode,
            Message = exception.Message
        };


        await context.Response.WriteAsync(errorDetails.ToString());
    }
}
public class ErrorDetails
{
    public int Error { get; set; }

    public string Message { get; set; }

    public override string ToString()
    {
        return this.toJsonStr();
    }
}


