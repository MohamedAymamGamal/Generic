namespace Ecom.Api.Helper
{
    public class ApiException : ResponseAPI
    {
        public ApiException(int statusCode, string message = null, string details=null) : base(statusCode, message)
        {
            Detail = details;
        }
        public string Detail { get; set; }
    }
}
