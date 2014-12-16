using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMTH.Data.Reports;
using SMTH.ViewModels.Reports;
using SMTH.Models;
using DA = SMTH.Data.Reports.DataAccess;

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
        [HttpPost]
        public ActionResult UsersReport(UsersReportViewModel model)
        {
            DA.LogReport(SessionWrapper.UserUuid, "UsersReport");
            return RedirectToAction("Index");
        }

        [AuthorizeUser]
        public ActionResult ReportsReport()
        {
            return View(new ReportsReportViewModel());
        }

        [AuthorizeUser]
        [HttpPost]
        public ActionResult ReportsReport(ReportsReportViewModel model)
        {
            DA.LogReport(SessionWrapper.UserUuid, "ReportsReport");
            return RedirectToAction("Index");
        }

        [AuthorizeUser]
        [HttpPost]
        public ActionResult ReportsReportDatePost(ReportsReportViewModel model)
        {
            model.usersJson = System.Web.Helpers.Json.Encode(new IncomingDto { data = DataAccess.GetReportsReportData(model.StartDate, model.EndDate), templateName = "ReportsReport" });
            model.isDateSelected = true;
            return View("ReportsReport", model);
        }
    }
}