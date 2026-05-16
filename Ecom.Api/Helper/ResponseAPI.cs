namespace Ecom.Api.Helper
{
    public class ResponseAPI
    {
        public ResponseAPI(int statusCode, string message=null, string token= null) 
        {
            StatusCode = statusCode;
            Message = message ?? GetMessageFromCode(StatusCode);
            Token = token;

        }
        private string GetMessageFromCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "OK",
                201 => "Created",
                400 => "Bad Request",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => "Unknown Status Code"
            };
        }
        public int StatusCode { get; set; }

        public string Token { get; set; }
        public string Message { get; set; }
    }
}
