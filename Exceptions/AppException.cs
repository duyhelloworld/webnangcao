using System.Net;

namespace webnangcao.Exceptions;

public class AppException : Exception
{
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
    public IEnumerable<string> Reasons { get; set; } = new List<string>();
    public IEnumerable<string>? RecommmendSolutions { get; set; } = new List<string>();
    public object? DataToFix { get; set; }

    public AppException(HttpStatusCode statusCode, string reason)
    {
        StatusCode = statusCode;
        Reasons = new List<string>() { reason };
    }

    public AppException(HttpStatusCode statusCode, string reason, string solution)
    {
        StatusCode = statusCode;
        Reasons = new List<string>() { reason };
        RecommmendSolutions = new List<string>() { solution };
    }

    public AppException(HttpStatusCode statusCode, IEnumerable<string> reasons, IEnumerable<string> recommendSolutions)
    {
        StatusCode = statusCode;
        Reasons = reasons;
        RecommmendSolutions = recommendSolutions;
    }
    
    public AppException(HttpStatusCode statusCode, IEnumerable<string> reasons, IEnumerable<string> recommendSolutions, object data)
    {
        StatusCode = statusCode;
        Reasons = reasons;
        RecommmendSolutions = recommendSolutions;
        DataToFix = data;
    }
}