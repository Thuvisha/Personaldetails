using Personaldetails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Personaldetails.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index(int id = 0)
        {
            return View(new details());
        }
        [HttpPost]
        public ActionResult Index(details model)
        {
            TempData["msg"] = "<script>alert('New User details Has Been Stored Sucessfully')</script>";

            HttpResponseMessage response = ConnectClass.client.PostAsJsonAsync("Personaldetails", model).Result;


            return View();
        }
        public ActionResult displaydetails()
        {
            IEnumerable<details> displaydetails;
            var response = ConnectClass.client.GetAsync("Personaldetails").Result;
            displaydetails = response.Content.ReadAsAsync<IEnumerable<details>>().Result;
            return View(displaydetails);


        }
        [HttpGet]
        public ActionResult Editdetails(int id)
        {
            HttpResponseMessage response = ConnectClass.client.GetAsync("Personaldetails/" + id.ToString()).Result;
            return View(response.Content.ReadAsAsync<details>().Result);
            //return View(/*new RolesTable()*/);
        }
        [HttpPost]
        public ActionResult Editdetails(details model)
        {
            var response = ConnectClass.client.PutAsJsonAsync("Personaldetails/" + model.Id, model).Result;
            //TempData["msg"] = "<script>alert('User details  has been changed sucessfully')</script>";
            return RedirectToAction("displaydetails");
        }
        //[HttpDelete]
        public ActionResult Deletedetails(int id)
        {
            var response = ConnectClass.client.DeleteAsync("Personaldetails/" + id.ToString()).Result;
            return RedirectToAction("displaydetails");
        }

    }
}
