

using NUnit.Framework;
using MyLeaveCare.Models;
using MyLeaveCare.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Microsoft.EntityFrameworkCore;
using MyLeaveCare.Repository;

namespace MyLeaveCareNUnit
{
    public class Tests
    {
        public List<EmployeeDetail> obj1 = new List<EmployeeDetail>();

      

        IQueryable<EmployeeDetail> cdata;
        Mock<DbSet<EmployeeDetail>> mockSet;
        Mock<TestLeaveManagementContext> carsinfocontextmock;
        [SetUp]
        public void Setup()
        {

            obj1 = new List<EmployeeDetail>()
            {
                new EmployeeDetail{ EmpId=101,EmpName="Radhika",EmpEmail="Radhika@gmail.com",EmpPhone="6292962692"  ,EmpPass="4343" 
                    
                    
                   }
            };
            cdata = obj1.AsQueryable();
            mockSet = new Mock<DbSet<EmployeeDetail>>();
            mockSet.As<IQueryable<EmployeeDetail>>().Setup(m => m.Provider).Returns(cdata.Provider);
            mockSet.As<IQueryable<EmployeeDetail>>().Setup(m => m.Expression).Returns(cdata.Expression);
            mockSet.As<IQueryable<EmployeeDetail>>().Setup(m => m.ElementType).Returns(cdata.ElementType);
            mockSet.As<IQueryable<EmployeeDetail>>().Setup(m => m.GetEnumerator()).Returns(cdata.GetEnumerator());
            var p = new DbContextOptions<TestLeaveManagementContext>();
            carsinfocontextmock = new Mock<TestLeaveManagementContext>(p);
            carsinfocontextmock.Setup(x => x.EmployeeDetails).Returns(mockSet.Object);

        }


        

        [Test]
        public void Test1()
        {
            var carsService = new EmployeeRepository(carsinfocontextmock.Object);
            var carslist = carsService.GetEmployeeDetails();
            Assert.AreEqual(1, carslist.Count());
        }


    }
}