using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Models
{
    public class Shipper
    {
        public Guid ShipperId { get; set; }
        public string? userId { get; set; }
        public string? ShipperStatus { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual User User { get; set; }
    }
}
