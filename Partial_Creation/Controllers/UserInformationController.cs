using Partial_Creation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Partial_Creation.Bussiness_Layer;
using System.Data;

namespace Partial_Creation.Controllers
{
    public class UserInformationController : Controller
    {       
        // GET: UserInformation
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Form(Users users)
        {
            if (ModelState.IsValid)
            {
                UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
                userBusinessLayer.AddUser(users);
                //return RedirectToAction("Display");
            }
                return PartialView();
        }

        public ActionResult Display(string EmailId)
        {
            UserBusinessLayer businessLayer = new UserBusinessLayer();
            //List<Users> users = businessLayer.users.ToList();
            DataTable data = UserBusinessLayer.getUserWithEmailId("df");
            Users dataout = new Users();
            dataout.EmailId = data.Rows[0]["EmailId"].ToString();
            dataout.slNo = (int)data.Rows[0]["slNo"];
            dataout.UserName = data.Rows[0]["UserName"].ToString();
            dataout.Password = data.Rows[0]["Password"].ToString();
            dataout.ConfirmPassword = data.Rows[0]["ConfirmPassword"].ToString();
            //Employee employee = eusmployeeContext.Employees.Single(x => x.EmployeeId == id);
            return PartialView(dataout);
        }

        //public ActionResult Display()
        //{
        //    return PartialView();
        //}
        //[HttpGet]
        //public ActionResult Save()
        //{
        //    return PartialView();
        //}

        //[HttpPost]
        public ActionResult Save(string name)
        {
            ViewBag.Message = "Do you want to save the data?";
            return PartialView();
        }
    }
}