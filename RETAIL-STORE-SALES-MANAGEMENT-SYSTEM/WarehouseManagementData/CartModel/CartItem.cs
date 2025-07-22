﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementData.CartModel
{
    public class CartItem
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal? Price { get; set; }

        public string ImageUrl { get; set; }

        public int Quantity { get; set; }
    }
}
