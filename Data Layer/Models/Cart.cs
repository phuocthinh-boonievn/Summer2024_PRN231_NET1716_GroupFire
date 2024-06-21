using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Models
{
    public class Cart
    {
        public Guid Id { get; set; }

        public Guid UserID { get; set; }
        public Guid ProductID { get; set; }
        public int Quannity { get; set; }
        
        public virtual User User { get; set; }
        public virtual MenuFoodItem Food { get; set; }
    }
}
