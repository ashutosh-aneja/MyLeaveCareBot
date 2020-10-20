using System;
using System.Collections.Generic;

namespace LeaveApply.Models
{
    public partial class EmployeeDetails
    {
        public EmployeeDetails()
        {
            ApplyLeave = new HashSet<ApplyLeave>();
            BalanceAvailable = new HashSet<BalanceAvailable>();
        }

        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpEmail { get; set; }
        public string EmpPhone { get; set; }
        public string EmpPass { get; set; }

        public virtual ICollection<ApplyLeave> ApplyLeave { get; set; }
        public virtual ICollection<BalanceAvailable> BalanceAvailable { get; set; }
    }
}
