using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using SMTH.Data.Reports;

namespace SMTH.ViewModels.Reports
{
    public class ReportsReportViewModel
    {
        public string data { get; set; }
        public string templateName { get; set; }
        public string generatorUrl { get; set; }

        public SelectList TypeList;

        [DisplayName("Report Type ")]
        public string ReportType { get; set; }

        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }

        public bool isDateSelected { get; set; }

        public ReportsReportViewModel()
        {
            templateName = "ReportsReport";
            TypeList = new SelectList(new List<String> { "Excel (.xlsx)" });
            generatorUrl = SMTH.Models.SessionWrapper.ReportsGeneratorPostUrl;
            StartDate = DateTime.Today.AddDays(-1);
            EndDate = DateTime.Today;
            data = Json.Encode(DataAccess.GetReportsReportData(StartDate, EndDate));
            isDateSelected = false;
        }
    }
}