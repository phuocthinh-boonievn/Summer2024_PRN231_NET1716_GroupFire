﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.ResourceModel.ViewModel.OrderDetailVMs
{
    public class OrderDetaiCreateVM
    {
        public Guid? OrderId { get; set; }
        public Guid? FoodId { get; set; }
        public int? Quantity { get; set; }
    }
}
