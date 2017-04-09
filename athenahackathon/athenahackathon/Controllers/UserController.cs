using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace athenahackathon.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult MyAccount()
        {
            return View();
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Mycloset()
        {
            return View();
        }


    }

  
}