namespace Presentation.Errors
{
    public class ApiServerErrorResponse : ApiResponse
    {
        public string? Details { get; set; }
        public ApiServerErrorResponse(int statusCode,string? message,string? details = null) : base(statusCode,message)
        {
            Details = details;
        }
    }
}
