using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyLeaveCareBot.Models;
using Newtonsoft.Json;

namespace MyLeaveCareBot.Controllers
{
    public class EmployeeManagementController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(EmployeeManagementController));
        private readonly TestLeaveManagementContext _context;

        public EmployeeManagementController(TestLeaveManagementContext context)
        {
            _context = context;
        }
      

        public IActionResult EmployeePortal()
        {
            //User data = (User)TempData["mydata"] ;

            User data = JsonConvert.DeserializeObject<User>(TempData["PopupMessages"].ToString());
            //Console.WriteLine(data.email);
            var res = _context.EmployeeDetails.FirstOrDefault(c => c.EmpEmail == data.email);

            //Console.WriteLine(res.EmpName);
            ViewData["EmployeeDetail"] = res;
            //Console.WriteLine(res.EmpName);
            return View();
        }
      
    }
}