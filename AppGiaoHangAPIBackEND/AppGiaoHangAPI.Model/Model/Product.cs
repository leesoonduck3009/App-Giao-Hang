using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGiaoHangAPI.Model.Model
{
    public partial class Product
    {
        public Product()
        {
            CustomerOrderDetails = new HashSet<CustomerOrderDetail>();
        }
        [Key]
        public long ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }

        public virtual ICollection<CustomerOrderDetail> CustomerOrderDetails { get; set; }
    }
}
