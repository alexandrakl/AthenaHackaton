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


        // GET: My Account -> Closet
        [AllowAnonymous]
        public ActionResult MyAccount()
        {
            string user = User.Identity.Name;
            return View();
        }

        // GET: My Closet Pass in User Id
        [AllowAnonymous]
        public ActionResult MyCloset()
        {
            return View();
        }

        // GET: My Closet Pass in User Id
        [AllowAnonymous]
        public ActionResult MyOutfit()
        {
            return View();
        }


    }

  
}