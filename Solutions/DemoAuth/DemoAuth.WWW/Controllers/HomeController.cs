using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace DemoAuth.WWW.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.WindowsIdentityName = WindowsIdentity.GetCurrent().Name;
            ViewBag.UserName = User.Identity.Name;
            ViewBag.UserIsAuthenticated = User.Identity.IsAuthenticated;
            ViewBag.UserAuthType = User.Identity.AuthenticationType;
            ViewBag.AuthenticationType = Helper.Config.AuthenticationType;
            ViewBag.Impersonate = Helper.Config.Impersonate;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}