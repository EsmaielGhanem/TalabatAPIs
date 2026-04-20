namespace Talabat.APIs.Errors;

public class ApiResponse
{
    public int  StatusCode { get; set; }
    public string Message { get; set; }
    public ApiResponse(int statusCode , string message = null )
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);

    }

    public string GetDefaultMessageForStatusCode(int statusCode)
    {
        if (statusCode == 400) return "A bad request , you have made"; 
        else if (statusCode == 401) return "Authorized , You are not";
        else if (statusCode == 404) return "Resource , it was not";
        else if (statusCode == 500) return "Error";
        return null;
    }
}