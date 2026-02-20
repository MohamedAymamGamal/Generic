namespace Ecom.Api.Sharing
{
    public class ProductParams
    {
        public string? Sort { get; set; } = null;

        public int? CategoryId { get; set; }
        public int MaxPageSize { get; set; } = 10;
        

        private int ? _pageSize=3;
        public int PageSize
        {
            get => _pageSize ?? MaxPageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        public int PageNumber { get; set; } = 1;
    }
}
