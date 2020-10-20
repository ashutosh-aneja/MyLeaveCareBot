

using NUnit.Framework;
using LeaveApply.Models;
using LeaveApply.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Microsoft.EntityFrameworkCore;
using LeaveApply.Repository;

namespace NUnitLeaveApply
{
    public class Tests
    {
        public List<ApplyLeave> obj1 = new List<ApplyLeave>();

      

        IQueryable<ApplyLeave> cdata;
        Mock<DbSet<ApplyLeave>> mockSet;
        Mock<TestLeaveManagementContext> carsinfocontextmock;
        [SetUp]
        public void Setup()
        {

            obj1 = new List<ApplyLeave>()
            {
                new ApplyLeave{ Applyid=101, EmpId=102,LeaveType="SickLeave",LeavestartDate=new DateTime(2020,10,27) , LeaveEndDate=new DateTime(2020,10,28) , LeaveReason="I am Sick" , LeaveId=102 , StatusId=0 , Status="Pending"
                
                }
               
            };
            cdata = obj1.AsQueryable();
            mockSet = new Mock<DbSet<ApplyLeave>>();
            mockSet.As<IQueryable<ApplyLeave>>().Setup(m => m.Provider).Returns(cdata.Provider);
            mockSet.As<IQueryable<ApplyLeave>>().Setup(m => m.Expression).Returns(cdata.Expression);
            mockSet.As<IQueryable<ApplyLeave>>().Setup(m => m.ElementType).Returns(cdata.ElementType);
            mockSet.As<IQueryable<ApplyLeave>>().Setup(m => m.GetEnumerator()).Returns(cdata.GetEnumerator());
            var p = new DbContextOptions<TestLeaveManagementContext>();
            carsinfocontextmock = new Mock<TestLeaveManagementContext>(p);
            carsinfocontextmock.Setup(x => x.ApplyLeave).Returns(mockSet.Object);

        }


        

        [Test]
        public void Test1()
        {
            var carsService = new ApplyLeaveRepository(carsinfocontextmock.Object);
            var carslist = carsService.GetApplyLeave();
            Assert.AreEqual(1, carslist.Count());
        }



       
    }
}