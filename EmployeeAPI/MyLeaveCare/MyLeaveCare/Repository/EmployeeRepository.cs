using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyLeaveCare.Models;

namespace MyLeaveCare.Repository
{
    public class EmployeeRepository : IEmployee
    {
        private readonly TestLeaveManagementContext _context;

        public EmployeeRepository(TestLeaveManagementContext context)
        {
           this._context = context;
        }
        public IEnumerable<EmployeeDetail> GetEmployeeDetails()
        {
           var res = _context.EmployeeDetails.ToList();
            return res;
        }

        public EmployeeDetail PostEmployeeDetailsAdd(EmployeeDetail employeeDetail)
        {
            var result = _context.EmployeeDetails.Add(employeeDetail);

            _context.SaveChanges();
            return employeeDetail;
        }

      

        public EmployeeDetail PutEmployee(int id, EmployeeDetail employeeDetail)
        {
            var res = _context.Entry(employeeDetail).State = EntityState.Modified;


            _context.SaveChangesAsync();
            return employeeDetail;
        }

        public EmployeeDetail DeleteEmployee(int id)
        {


            EmployeeDetail employeeDetail = _context.EmployeeDetails.Find(id);


            _context.EmployeeDetails.Remove(employeeDetail);
            _context.SaveChanges();

            return employeeDetail;
        }
    }
}
