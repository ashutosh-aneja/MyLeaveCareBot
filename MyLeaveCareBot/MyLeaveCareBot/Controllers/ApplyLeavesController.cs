using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLeaveCareBot.Models;

namespace MyLeaveCareBot.Controllers
{
    public class ApplyLeavesController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ApplyLeavesController));
        private readonly TestLeaveManagementContext _context;

        public ApplyLeavesController(TestLeaveManagementContext context)
        {
            _context = context;
        }
        public IActionResult EmployeePortal1()
        {


            return View();
        }
        // GET: ApplyLeaves
        public async Task<IActionResult> Index()
        {
            var testLeaveManagementContext = _context.ApplyLeaves.Include(a => a.Emp).Include(a => a.Leave);
            return View(await testLeaveManagementContext.ToListAsync());
        }

        // GET: ApplyLeaves/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applyLeave = await _context.ApplyLeaves
                .Include(a => a.Emp)
                .Include(a => a.Leave)
                .FirstOrDefaultAsync(m => m.Applyid == id);
            if (applyLeave == null)
            {
                return NotFound();
            }

            return View(applyLeave);
        }

        // GET: ApplyLeaves/Create
        public IActionResult Create()
        {
            ViewData["EmpId"] = new SelectList(_context.EmployeeDetails, "EmpId", "EmpId");
            ViewData["LeaveId"] = new SelectList(_context.BalanceAvailables, "LeaveId", "LeaveId");
          //  ViewData["LeaveType"] = new SelectList("Sick Leave","Vacation Leave");
            return View();
        }

        // POST: ApplyLeaves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Applyid,EmpId,LeaveType,LeavestartDate,LeaveEndDate,LeaveReason,LeaveId,StatusId,Status")] ApplyLeave applyLeave)
        {

            //Not Working as some SSL SOcket Connection Problem
            //using (var httpClient = new HttpClient())
            //{
            //    StringContent content = new StringContent(JsonConvert.SerializeObject(employeeDetail), Encoding.UTF8, "application/json");
            //    using (var response = await httpClient.PostAsync("http://localhost:44349/api/EmployeeDetailsAdd/EmpAdd", content))
            //    {
            //        string apiResponse = await response.Content.ReadAsStringAsync();
            //        EmployeeDetail Emplo = JsonConvert.DeserializeObject<EmployeeDetail>(apiResponse);
            //    }
            //}




            applyLeave.LeaveId = applyLeave.EmpId;
            if (ModelState.IsValid)
            {
                _context.Add(applyLeave);
                await _context.SaveChangesAsync();
                // return RedirectToAction(nameof(Index));
                return RedirectToAction("EmployeePortal","EmployeeManagement");
            }
            ViewData["EmpId"] = new SelectList(_context.EmployeeDetails, "EmpId", "EmpEmail", applyLeave.EmpId);
            ViewData["LeaveId"] = new SelectList(_context.BalanceAvailables, "LeaveId", "LeaveId", applyLeave.LeaveId);
            return View(applyLeave);
        }

        // GET: ApplyLeaves/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applyLeave = await _context.ApplyLeaves.FindAsync(id);
            if (applyLeave == null)
            {
                return NotFound();
            }
            ViewData["EmpId"] = new SelectList(_context.EmployeeDetails, "EmpId", "EmpName", applyLeave.EmpId);
            ViewData["LeaveId"] = new SelectList(_context.BalanceAvailables, "LeaveId", "LeaveId", applyLeave.LeaveId);
            return View(applyLeave);
        }

        // POST: ApplyLeaves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Applyid,EmpId,LeaveType,LeavestartDate,LeaveEndDate,LeaveReason,LeaveId,StatusId,Status")] ApplyLeave applyLeave)
        {
            if (id != applyLeave.Applyid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applyLeave);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplyLeaveExists(applyLeave.Applyid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpId"] = new SelectList(_context.EmployeeDetails, "EmpId", "EmpEmail", applyLeave.EmpId);
            ViewData["LeaveId"] = new SelectList(_context.BalanceAvailables, "LeaveId", "LeaveId", applyLeave.LeaveId);
            return View(applyLeave);
        }

        // GET: ApplyLeaves/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applyLeave = await _context.ApplyLeaves
                .Include(a => a.Emp)
                .Include(a => a.Leave)
                .FirstOrDefaultAsync(m => m.Applyid == id);
            if (applyLeave == null)
            {
                return NotFound();
            }

            return View(applyLeave);
        }

        // POST: ApplyLeaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applyLeave = await _context.ApplyLeaves.FindAsync(id);
            _context.ApplyLeaves.Remove(applyLeave);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplyLeaveExists(int id)
        {
            return _context.ApplyLeaves.Any(e => e.Applyid == id);
        }
       
    }
}
