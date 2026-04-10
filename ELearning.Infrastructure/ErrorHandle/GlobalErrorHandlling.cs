namespace ELearning.Infrastructure.ErrorHandle
{
    public class GlobalErrorHandlling : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var detials = new ProblemDetails()
            {
                Detail = $"Api Error {exception.Message}",
                Instance = "API",
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "Api Error: ",
                Type = "Server Error"
            };

            var resopnse = JsonSerializer.Serialize(detials);

            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsync(resopnse, cancellationToken);

            return true;
        }
    }
}
