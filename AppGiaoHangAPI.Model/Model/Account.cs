using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGiaoHangAPI.Model.Model
{
    public partial class Account
    {
        [Key]
        public long AccountId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? AccountCreateTime { get; set; }
        public string? Roles { get; set; }
        public long? EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
