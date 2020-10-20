using System;
using System.Collections.Generic;

namespace LeaveApply.Models
{
    public partial class BalanceAvailable
    {
        public BalanceAvailable()
        {
            ApplyLeave = new HashSet<ApplyLeave>();
        }

        public int LeaveId { get; set; }
        public double SickBalance { get; set; }
        public double VacationBalance { get; set; }
        public double CustomLeave { get; set; }
        public double TotalLeaveTaken { get; set; }
        public int? EmpId { get; set; }

        public virtual EmployeeDetails Emp { get; set; }
        public virtual ICollection<ApplyLeave> ApplyLeave { get; set; }
    }
}
