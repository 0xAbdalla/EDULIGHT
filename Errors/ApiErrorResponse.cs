namespace EDULIGHT.Errors
{
    public class ApiErrorResponse
    {
        private readonly int statuscode;

        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ApiErrorResponse(int statuscode,string? message = null)
        {
            StatusCode = statuscode;
            Message = message ?? GetDefualtMessageForStatusCode(statuscode);
        }
        private string? GetDefualtMessageForStatusCode(int statuscode) 
        {
            var message = statuscode switch
            {
                400 => "A Bad Request.",
                401 => "You do not have authorized.",
                404 => "Resource was not found.",
                500 => "Server Error",
                _ => null
            };
            return message;
        }
    }
}
