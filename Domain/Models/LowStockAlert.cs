using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class LowStockAlert
    {
        public int Id { get; set; }
        public int Threshold { get; set; }
        public bool AlertSent { get; set; }
        public DateTime Date { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

    }
}
