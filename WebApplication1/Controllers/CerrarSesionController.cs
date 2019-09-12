using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class CerrarSesionController : Controller
    {
        // GET: CerrarSesion
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CerrarSession()
        {
            Session.Clear();
            Session.Abandon();
            Session["usuario"] = null;
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
            return View();
        }
    }
}