using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using SMTH.Data.Reports;

namespace SMTH.ViewModels.Reports
{
    public class UsersReportViewModel
    {
        public string data { get; set; }
        public string templateName { get; set; }
        public string generatorUrl { get; set; }

        public SelectList TypeList;

        [DisplayName("Report Type ")]
        public string ReportType { get; set; }

        public UsersReportViewModel()
        {
            templateName = "UsersReport";
            TypeList = new SelectList(new List<String> { "Excel (.xlsx)" });
            generatorUrl = SMTH.Models.SessionWrapper.ReportsGeneratorPostUrl;
            data = Json.Encode(DataAccess.GetUsersReportData());
        }


    }
}