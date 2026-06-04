using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.DTO
{
    public record CategoryDTO
   (
    string Name,
    string Description
    
   );

    public record CategoryWithProductsDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<ProductDto> Products { get; set; } = new();
    }

    public record CategoryUpdateDTO(string Name, string Description, int id);
}
