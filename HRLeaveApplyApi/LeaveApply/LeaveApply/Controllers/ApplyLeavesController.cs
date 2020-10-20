using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeaveApply.Models;
using LeaveApply.Repository;

namespace LeaveApply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplyLeavesController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ApplyLeavesController));

        private readonly ILeave _LeaveRepository;
        public ApplyLeavesController(ILeave LeaveRepo)
        {
            _LeaveRepository = LeaveRepo;

        }
        // GET: api/<PgController>
        [HttpGet]
        public IActionResult GetLeave()
        {
            try
            {
                _log4net.Info(" Http GET is accesed");
                IEnumerable<ApplyLeave> res = _LeaveRepository.GetApplyLeave();
                return Ok(res);
            }
            catch
            {
                _log4net.Error("Error in Get request");
                return new NoContentResult();
            }
        }



        [HttpPost]
        public IActionResult Post([FromBody]ApplyLeave applyLeave)
        {
            
                _log4net.Info("Employee Details Getting Added");
                if (ModelState.IsValid)
                {

                    var res = _LeaveRepository.PostLeavesAdd(applyLeave);


                    return Ok();

                }
                return BadRequest();

            
         }


        [HttpPut("{id}")]
        public IActionResult PutApplyLeave(int id, ApplyLeave applyLeave)
        {
            if (id != applyLeave.Applyid)
            {
                return BadRequest();
            }



            try
            {
                _log4net.Info("Leave Update Details Getting Added");
                if (ModelState.IsValid)
                {

                    var res = _LeaveRepository.PutApplyLeave(id, applyLeave);


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
            ApplyLeave applyLeave = _LeaveRepository.DeleteApplyLeave(id);
            return Ok();
        }


    }




    //private bool ApplyLeaveExists(int id)
    //{
    //    return _context.ApplyLeave.Any(e => e.Applyid == id);
    //}
}

