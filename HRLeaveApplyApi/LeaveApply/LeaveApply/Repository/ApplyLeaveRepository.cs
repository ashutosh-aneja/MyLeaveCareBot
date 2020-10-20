using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveApply.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveApply.Repository
{
    public class ApplyLeaveRepository : ILeave
    {
        private readonly TestLeaveManagementContext _context;

        public ApplyLeaveRepository(TestLeaveManagementContext context)
        {
            _context = context;
        }

      

        public IEnumerable<ApplyLeave> GetApplyLeave()
        {
            var res = _context.ApplyLeave.ToList();
            return res;
        }

       
        public ApplyLeave PostLeavesAdd(ApplyLeave applyLeave)
        {
            var result = _context.ApplyLeave.Add(applyLeave);

           var a= _context.SaveChanges();
            return applyLeave;
        }

        public ApplyLeave PutApplyLeave(int id, ApplyLeave applyLeave)
        {
          var res=  _context.Entry(applyLeave).State = EntityState.Modified;

            
             _context.SaveChangesAsync();
            return applyLeave;
           
           
        }


        public ApplyLeave DeleteApplyLeave(int id)
        {
            ApplyLeave applyLeave =  _context.ApplyLeave.Find(id);
           

               _context.ApplyLeave.Remove(applyLeave);
               _context.SaveChanges();

            return applyLeave;
        }
    }
}
