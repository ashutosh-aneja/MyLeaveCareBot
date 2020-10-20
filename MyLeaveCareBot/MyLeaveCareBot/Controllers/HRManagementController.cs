using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeaveCareBot.Models;
using MyLeaveCareBot.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyLeaveCareBot.Controllers
{
    public class HRManagementController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(HRManagementController));
        // private readonly TestLeaveManagementContext context;

        // private readonly HREmployeeAddRepository _HREmployeeAddRepository ;
        //public HRManagementController()
        //{
        //    _HREmployeeAddRepository = new HREmployeeAddRepository( );
        //}
        private readonly TestLeaveManagementContext _context;

        ////public HREmployeeAddRepository()
        ////{
        ////}

        public HRManagementController(TestLeaveManagementContext context)
        {
            
            _context = context;
            
        }
        public HREmployeeAddRepository _HREmployeeAddRepository = new HREmployeeAddRepository();

       

        
        public IActionResult HRPortal()
        {
            return View();
        }
        public IActionResult HRSeeLeaves()
        {
            return RedirectToAction("Index","ApplyLeaves");
        }

        public IActionResult Index()
        {
           // User obj = new User { email = "admin", password = "admin" };
            using (HttpClient client = new HttpClient())
            {
             //   var token = GetToken("https://localhost:44316/api/Token", obj);
                client.BaseAddress = new Uri("https://localhost:44349/api/");
                // MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                //client.DefaultRequestHeaders.Accept.Add(contentType);
              //  client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = new HttpResponseMessage();
                response = client.GetAsync("EmployeeDetailsAdd/GetDet").Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<IEnumerable<EmployeeDetail>>(stringData);
                return View(data);
            }
        }

      
        static string GetToken(string url, User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = client.PostAsync(url, data).Result;
                string name = response.Content.ReadAsStringAsync().Result;
                dynamic details = JObject.Parse(name);
                return details.token;
            }
        }












        public IActionResult HRSeeDetails()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349/api/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = new HttpResponseMessage();
                response = client.GetAsync("EmployeeDetailsAdd").Result;


                string stringData = response.Content.ReadAsStringAsync().Result;

                var data = JsonConvert.DeserializeObject<IEnumerable<EmployeeDetail>>(stringData);


                return View(data);
            }

        }
        public IActionResult HRSeeLeavesfromapi()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44336/api/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = new HttpResponseMessage();
                response = client.GetAsync("ApplyLeaves").Result;


                string stringData = response.Content.ReadAsStringAsync().Result;

                var data = JsonConvert.DeserializeObject<IEnumerable<ApplyLeave>>(stringData);


                return View(data);
            }

        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: HR/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost] //async Task<IActionResult>
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("EmpId,EmpName,EmpEmail,EmpPhone,EmpPass")] EmployeeDetail employeeDetail)
        { 
            if (employeeDetail != null)
            {

               
                var res = _HREmployeeAddRepository.PostEmployee(employeeDetail,_context);


                //  return Ok();
                // return new HttpResponseMessage(HttpStatusCode.OK);
                return RedirectToAction("HRPortal","HRManagement");
               
            }
            else
            {
                //  return BadRequest();
                return RedirectToAction("HRPortal", "HRManagement");
              //  return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                


            }
        }








    }
}