using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGiaoHangAPI.Model.Model
{
    public partial class CustomerOrderInformation
    {
        public CustomerOrderInformation()
        {
            CustomerOrders = new HashSet<CustomerOrder>();
        }
        [Key]

        public long CustomerOrderInformationId { get; set; }
        public long? CustomerId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual ICollection<CustomerOrder> CustomerOrders { get; set; }
    }
}
