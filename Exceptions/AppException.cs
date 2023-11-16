using System.Net;

namespace webnangcao.Exceptions;

public class AppException : Exception
{
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
    public string Reason { get; set; } = "";
    public string? RecommmendSolution { get; set; } = "";
    public object? DataToFix { get; set; }

    public AppException(HttpStatusCode statusCode, string reason)
    {
        StatusCode = statusCode;
        Reason = reason;
    }

    public AppException(HttpStatusCode statusCode, string reason, string solution)
        : this(statusCode, reason)
    {
        RecommmendSolution = solution;
    }

    public AppException(HttpStatusCode statusCode, string reason, string recommendSolution, object data)
        : this(statusCode, reason, recommendSolution)
    {
        DataToFix = data;
    }
}