using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGiaoHangAPI.Model.Model
{
    public class CustomerOrder
    {
        public long CustomerOrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public long CustomerOrderInformationID { get; set; }
        public long EmployeeID { get; set; }
        public double TotalPrice { get; set; }
        public string OrderStatus { get; set; }

        public CustomerOrderInformation CustomerOrderInformation { get; set; }
        public Employee Employee { get; set; }
        public ICollection<CustomerOrderDetail> CustomerOrderDetails { get; set; }
    }
}
