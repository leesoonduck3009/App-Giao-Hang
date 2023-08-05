using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGiaoHangAPI.Model.Model
{
    public class Product
    {
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public ICollection<CustomerOrderDetail> CustomerOrderDetails { get; set; }
    }
}
}
