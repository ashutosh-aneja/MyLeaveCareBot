using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveApply.Models;

namespace LeaveApply.Repository
{
   public interface ILeave
    {
      public IEnumerable<ApplyLeave> GetApplyLeave();
       public ApplyLeave PostLeavesAdd(ApplyLeave applyLeave);
        public ApplyLeave PutApplyLeave(int id, ApplyLeave applyLeave);
        public ApplyLeave DeleteApplyLeave(int id);
    }
}
