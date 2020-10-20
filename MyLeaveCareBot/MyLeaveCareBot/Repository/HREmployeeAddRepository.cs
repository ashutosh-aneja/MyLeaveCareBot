using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyLeaveCareBot.Models;

namespace MyLeaveCareBot.Repository
{
    public class HREmployeeAddRepository
    {
        

        public dynamic PostEmployee(EmployeeDetail employeeDetail, TestLeaveManagementContext _context)
        {
            try
            {
                var res = _context.Database.ExecuteSqlRaw("AddDetailEmployee {0} , {1} , {2} ,{3} , {4}", employeeDetail.EmpId, employeeDetail.EmpName, employeeDetail.EmpEmail, employeeDetail.EmpPhone, employeeDetail.EmpPass);


                return res;

            }
            catch (Exception ex)
            {
                return null;
            }
        }





    }
}
