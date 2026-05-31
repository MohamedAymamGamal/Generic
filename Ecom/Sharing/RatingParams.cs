using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Sharing
{
    public class RatingParams
    {

        // newest | oldest | highest | lowest
        public string? Sort { get; set; } = "newest";

        public int TotalCount { get; set; }

        public int MaxPageSize { get; set; } = 200;

        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize
                ? MaxPageSize
                : value;
        }

        public int PageNumber { get; set; } = 1;




    }
}