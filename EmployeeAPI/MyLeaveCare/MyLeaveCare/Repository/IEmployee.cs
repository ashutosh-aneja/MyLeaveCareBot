using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyLeaveCare.Models;

namespace MyLeaveCare.Repository
{
  public interface IEmployee
    {
        IEnumerable<EmployeeDetail> GetEmployeeDetails();
        EmployeeDetail PostEmployeeDetailsAdd(EmployeeDetail employeeDetail);
        public EmployeeDetail PutEmployee(int id, EmployeeDetail employeeDetail);
        public EmployeeDetail DeleteEmployee(int id);
    }
}
