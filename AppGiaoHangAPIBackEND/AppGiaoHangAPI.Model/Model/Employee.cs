using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGiaoHangAPI.Model.Model
{
    public partial class Employee
    {
        public Employee()
        {
            Accounts = new HashSet<Account>();
            CustomerOrders = new HashSet<CustomerOrder>();
        }
        [Key]
        public long EmployeeId { get; set; }
        public string? EmployeeCode { get; set; }
        public string? EmployeeName { get; set; }
        public DateTime? DateJoin { get; set; }
        public string? PhoneNumber { get; set; }
        public string? IdentityNumber { get; set; }
        public DateTime? Birthday { get; set; }
        public double? LastLatitude { get; set; }
        public double? LastLongitude { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<CustomerOrder> CustomerOrders { get; set; }
    }
}
