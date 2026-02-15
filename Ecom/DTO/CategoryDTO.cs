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

    public record CategoryUpdateDTO(string Name, string Description, int id);
}
