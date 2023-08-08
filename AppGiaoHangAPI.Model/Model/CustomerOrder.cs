using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGiaoHangAPI.Model.Model
{
    public partial class CustomerOrder
    {
        public CustomerOrder()
        {
            CustomerOrderDetails = new HashSet<CustomerOrderDetail>();
        }
        [Key]
        public long CustomerOrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public long? CustomerOrderInformationId { get; set; }
        public long? EmployeeId { get; set; }
        public double? TotalPrice { get; set; }
        public string? OrderStatus { get; set; }

        public virtual CustomerOrderInformation? CustomerOrderInformation { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual ICollection<CustomerOrderDetail> CustomerOrderDetails { get; set; }
    }
}
