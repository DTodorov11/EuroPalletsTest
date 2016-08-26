using EuroPallets.Models;
using EuroPallets.Services;
using EuroPallets.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuroPallets.Controllers
{
    public class TestController : Controller
    {
        private IUserServices userServices;

        public TestController(IUserServices userServices)
        {
            this.userServices = userServices;
        }

        // GET: Test
        public ActionResult Index()
        {
            var currentUsers = this.userServices.TakeUserByUserName("");

            return View();
        }
    }
}