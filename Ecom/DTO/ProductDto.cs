using Ecom.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Http;
namespace Ecom.Core.DTO
{
    public record  ProductDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public virtual List<PhotoDto> Photos { get; set; } 

        public decimal? Newprice { get; set; }
        public decimal? OldPrice { get; set; }
        public string CategoryName  { get; set; }
    }
    public record PhotoDto
    {
        public string? ImageName { get; set; }
        public int ProductId { get; set; }  
    }

    public record ReturnProductDto
    {
        public List<ProductDto> products { get; set; }
        public int TotalCount { get; set; }
    }
    public record AddProductDto
    {
        public string ? Name { get; set; }
        public string? Description { get; set; }
        public decimal? NewPrice { get; set; }
        public decimal ? OldPrice { get; set; }
         
        public int CategoryId { get; set; }
        public  IFormFileCollection Photo { get; set; }

    }
    public record UpdateProductDto : AddProductDto
    {
      public int Id { get; set; }

    }
}
