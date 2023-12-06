using System.Net.Mime;

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
                Reason = ex.Reason,
                RecommmendSolution = ex.RecommmendSolution,
            });
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = MediaTypeNames.Application.Json;
            await context.Response.WriteAsJsonAsync(new ResponseError()
            {
                Reason = "Có lỗi xảy ra với hệ thống",
                RecommmendSolution = "Vui lòng liên hệ admin để biết thêm chi tiết."
            });
            Console.WriteLine(ex.ToString());
        }
    }
}