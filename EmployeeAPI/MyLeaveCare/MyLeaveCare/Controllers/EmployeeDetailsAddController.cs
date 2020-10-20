using System;
//using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeaveCare.Models;
using MyLeaveCare.Repository;

namespace MyLeaveCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDetailsAddController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(EmployeeDetailsAddController));

        //public EmployeeDetailsAddController()
        //{

        //}

        private readonly IEmployee _EmpRepository;
        public EmployeeDetailsAddController(IEmployee empRepo)
        {
            _EmpRepository = empRepo;

        }
        // GET: api/<PgController>
        [HttpGet]
        public IActionResult GetEmp()
        {
            try
            {
                _log4net.Info(" Http GET is accesed");
                IEnumerable<EmployeeDetail> res = _EmpRepository.GetEmployeeDetails();
                return Ok(res);
            }
            catch
            {
                _log4net.Error("Error in Get request");
                return new NoContentResult();
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody]EmployeeDetail employeeDetail)
        {
            try
            {
                _log4net.Info("Employee Details Getting Added");
                if (ModelState.IsValid)
                {

                    var bookobj = _EmpRepository.PostEmployeeDetailsAdd(employeeDetail);
                    

                    return Ok();

                }
                return BadRequest();


            }
            catch
            {
                _log4net.Error("Error in Adding Employee Details");
                return new NoContentResult();
            }

        }
        [HttpPut("{id}")]
        public IActionResult PutEmployee(int id, EmployeeDetail employeeDetail)
        {
            if (id != employeeDetail.EmpId)
            {
                return BadRequest();
            }



            try
            {
                _log4net.Info("Leave Update Details Getting Added");
                if (ModelState.IsValid)
                {

                    var res = _EmpRepository.PutEmployee(id, employeeDetail);


                    return Ok();

                }
                return BadRequest();


            }
            catch
            {
                _log4net.Error("Error in Updating Leaves Details");
                return new NoContentResult();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteApplyLeavee(int id)
        {
            EmployeeDetail applyLeave = _EmpRepository.DeleteEmployee(id);
            return Ok();
        }

        

        //private bool EmployeeDetailExists(int id)
        //{
        //    return _context.EmployeeDetails.Any(e => e.EmpId == id);
        //}
    }
}
