using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGiaoHangAPI.Model.Model
{
    public partial class CustomerOrderDetail
    {
        [Key]
        public long CustomerOrderDetailId { get; set; }
        public long? ProductId { get; set; }
        public long? CustomerOrderId { get; set; }
        public int? Quantity { get; set; }
        public double? OrderDetailPrice { get; set; }

        public virtual CustomerOrder? CustomerOrder { get; set; }
        public virtual Product? Product { get; set; }
    }
}
