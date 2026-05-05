namespace Ecom.Api.Sharing
{
    public class ProductParams
    {
        public string? Sort { get; set; }
        public int? CategoryId { get; set; }

        public int TotalCount { get; set; }

        public string? Search { get; set; }
        public int MaxPageSize { get; set; } = 200;
        
        private int _pageSize = 3;

        public int pageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }
        public int PageNumber { get; set; } = 1;
    }
}
