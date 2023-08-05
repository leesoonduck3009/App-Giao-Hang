using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGiaoHangAPI.Model.Model
{
    public class CustomerOrderDetail
    {
        public long CustomerOrderDetailID { get; set; }
        public long ProductID { get; set; }
        public long CustomerOrderID { get; set; }
        public int Quantity { get; set; }
        public double OrderDetailPrice { get; set; }

        public Product Product { get; set; }
        public CustomerOrder CustomerOrder { get; set; }
    }
}
