using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMTH.Models;
using SMTH.ViewModels;

namespace SMTH.Controllers
{
    public class HomeController : Controller
    {
        [AuthorizeUser]
        public ActionResult Index()
        {
            return View();
        }
    }
}