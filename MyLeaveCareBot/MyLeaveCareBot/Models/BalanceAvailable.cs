using System;
using System.Collections.Generic;

#nullable disable

namespace MyLeaveCareBot.Models
{
    public partial class BalanceAvailable
    {
        public BalanceAvailable()
        {
            ApplyLeaves = new HashSet<ApplyLeave>();
        }

        public int LeaveId { get; set; }
        public double SickBalance { get; set; }
        public double VacationBalance { get; set; }
        public double CustomLeave { get; set; }
        public double TotalLeaveTaken { get; set; }
        public int? EmpId { get; set; }

        public virtual EmployeeDetail Emp { get; set; }
        public virtual ICollection<ApplyLeave> ApplyLeaves { get; set; }
    }
}
