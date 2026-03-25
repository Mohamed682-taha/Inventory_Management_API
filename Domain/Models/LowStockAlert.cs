using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class LowStockAlert : BaseEntity<int>
    {
        public int Threshold { get; set; }
        public bool AlertSent { get; set; } = false;
        public DateTime Date { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

    }
}
