using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMTH.ViewModels.Reports;
using SMTH.Models;

namespace SMTH.Controllers
{
    public class ReportsController : Controller
    {
        [AuthorizeUser]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUser]
        public ActionResult LogisticsReport()
        {
            return View(new LogisticsReportViewModel());
        }

        [AuthorizeUser]
        public ActionResult ProductionReport()
        {
            return View(new ProductionReportViewModel());
        }

        [AuthorizeUser]
        public ActionResult ProfitReport()
        {
            return View(new ProfitReportViewModel());
        }

        [AuthorizeUser]
        public ActionResult UsersReport()
        {
            return View(new UsersReportViewModel());
        }

        [AuthorizeUser]
        public ActionResult ReportsReport()
        {
            return View(new ReportsReportViewModel());
        }
    }
}