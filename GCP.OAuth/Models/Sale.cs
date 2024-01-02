using System;
using System.Collections.Generic;

namespace GCP.OAuth.Models
{
    public partial class Sale
    {
        public int Pk { get; set; }
        public string Name { get; set; } = null!;
        public string Item { get; set; } = null!;
        public int Qty { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
