using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGiaoHangAPI.Model.Model
{
    public class Account
    {
        public long AccountID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime AccountCreateTime { get; set; }
        public string Roles { get; set; }
        public long EmployeeID { get; set; }

        public Employee Employee { get; set; }
    }
}
