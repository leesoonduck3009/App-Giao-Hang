using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGiaoHangAPI.Model.Model
{
    public class Customer
    {
        public long CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public DateTime DateCreate { get; set; }
        public string CustomerName { get; set; }
        public DateTime Birthday { get; set; }
        public string CustomerRank { get; set; }

        public ICollection<CustomerOrder> CustomerOrders { get; set; }
    }
}
