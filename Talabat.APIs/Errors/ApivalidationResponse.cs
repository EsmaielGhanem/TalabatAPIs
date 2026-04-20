namespace Talabat.APIs.Errors;

public class ApivalidationResponse : ApiResponse
{
     // 1-status code 
     // 2-message 
     // 1-2 from apiResponse 
     //3-Error (List)
     public IEnumerable<string> Errors { get; set; }

     public ApivalidationResponse() : base(400)
     {
          // Errors ????!!!!
     }
}