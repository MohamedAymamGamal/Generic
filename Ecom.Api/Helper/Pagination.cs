namespace Ecom.Api.Helper
{
    public class Pagination<T> where T : class
    {
        public int PageNumber { get; set; } 
        public int PageSize { get; set; }

        public int TotalItems { get; set; } 
    }
}
