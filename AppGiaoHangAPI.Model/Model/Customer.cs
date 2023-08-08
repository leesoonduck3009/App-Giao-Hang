using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGiaoHangAPI.Model.Model
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerOrderInformations = new HashSet<CustomerOrderInformation>();
        }
        [Key]

        public long CustomerId { get; set; }
        public string? CustomerCode { get; set; }
        public DateTime? DateCreate { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? Birthday { get; set; }
        public string? CustomerRank { get; set; }

        public virtual ICollection<CustomerOrderInformation> CustomerOrderInformations { get; set; }
    }
}
