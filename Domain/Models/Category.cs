using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<Product> Products { get; set; } = null!;
    }
}
