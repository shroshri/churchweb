using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcChurchsj.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "hello index";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }
        public ActionResult Newsevent()
        {
            ViewBag.Message = "Your ne page.";

            return View();
        }
        public ActionResult Service()
        {
            ViewBag.Message = "Your ser page.";

            return View();
        }
        public ActionResult Activity()
        {
            ViewBag.Message = "Your act page.";

            return View();
        }

        public ActionResult Contact()
        {
           

            return View();
        }
        public ActionResult our_people()
        {


            return View();
        }
        public ActionResult Contact_parish()
        {


            return View();
        }
        public ActionResult school()
        {


            return View();
        }
        public ActionResult newsletter()
        {


            return View();
        }
        public ActionResult pic_gallery()
        {


            return View();
        }
        public ActionResult History()
        {


            return View();
        }
    }
}
