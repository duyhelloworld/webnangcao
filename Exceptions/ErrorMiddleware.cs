
namespace webnangcao.Exceptions;

public class ErrorMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (AppException ex)
        {
            context.Response.StatusCode = (int) ex.StatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new ResponseError()
            {
                Reasons = ex.Reasons,
                RecommmendSolutions = ex.RecommmendSolutions,
                DataToFix = ex.DataToFix
            });
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new ResponseError()
            {
                Reasons = new List<string>() {"Có lỗi xảy ra với hệ thống"},
                RecommmendSolutions = new List<string>() { "Vui lòng liên hệ admin để biết thêm chi tiết." }
            });
            Console.WriteLine(ex.ToString());
        }
    }
}