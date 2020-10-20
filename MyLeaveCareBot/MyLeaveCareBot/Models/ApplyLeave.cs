using System;
using System.Collections.Generic;

#nullable disable

namespace MyLeaveCareBot.Models
{
    public partial class ApplyLeave
    {
        public int Applyid { get; set; }
        public int? EmpId { get; set; }
        public string LeaveType { get; set; }
        public DateTime LeavestartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public string LeaveReason { get; set; }
        public int? LeaveId { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }

        public virtual EmployeeDetail Emp { get; set; }
        public virtual BalanceAvailable Leave { get; set; }
    }
}
