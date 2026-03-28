using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.ProductsDto
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public int CategoryId { get; set; }
        public string Supplier { get; set; } = string.Empty;
    }
}
