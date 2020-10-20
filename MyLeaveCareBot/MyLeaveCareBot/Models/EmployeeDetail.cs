using System;
using System.Collections.Generic;

#nullable disable

namespace MyLeaveCareBot.Models
{
    public partial class EmployeeDetail
    {
        public EmployeeDetail()
        {
            ApplyLeaves = new HashSet<ApplyLeave>();
            BalanceAvailables = new HashSet<BalanceAvailable>();
        }

        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpEmail { get; set; }
        public string EmpPhone { get; set; }
        public string EmpPass { get; set; }

        public virtual ICollection<ApplyLeave> ApplyLeaves { get; set; }
        public virtual ICollection<BalanceAvailable> BalanceAvailables { get; set; }
    }
}
