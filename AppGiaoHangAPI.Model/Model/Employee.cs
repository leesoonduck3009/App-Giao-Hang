using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGiaoHangAPI.Model.Model
{
    public class Employee
    {
        public long EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DateJoin { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime Birthday { get; set; }

        public ICollection<Account> Accounts { get; set; }
        public ICollection<CustomerOrder> CustomerOrders { get; set; }
    }
}
