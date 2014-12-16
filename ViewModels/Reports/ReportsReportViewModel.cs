using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using SMTH.Data.Reports;

namespace SMTH.ViewModels.Reports
{
    public class ReportsReportViewModel
    {
        public string usersJson { get; set; }
        public string generatorUrl { get; set; }

        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }

        public bool isDateSelected { get; set; }

        public ReportsReportViewModel()
        {
            generatorUrl = "http://localhost:20313/api/Service";
            StartDate = DateTime.Today.AddDays(-1);
            EndDate = DateTime.Today;
            usersJson = Json.Encode(new IncomingDto { data = DataAccess.GetReportsReportData(StartDate, EndDate), templateName = "ReportsReport"});
            isDateSelected = false;
        }
    }
}