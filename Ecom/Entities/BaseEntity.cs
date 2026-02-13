using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Entities
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
    }
}
