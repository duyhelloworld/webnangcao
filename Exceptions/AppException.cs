using System.Net;

namespace webnangcao.Exceptions;

public class AppException : Exception
{
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
    public string Reason { get; set; } = null!;
    public string? RecommmendSolution { get; set; }

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
}