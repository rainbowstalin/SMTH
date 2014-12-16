using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using SMTH.Data.Reports;

namespace SMTH.ViewModels.Reports
{
    public class UsersReportViewModel
    {
        public string usersJson { get; set; }
        public string generatorUrl { get; set; }

        public UsersReportViewModel()
        {
            generatorUrl = "http://localhost:20313/api/Service";
            usersJson = Json.Encode(new IncomingDto { data = DataAccess.GetUsersReportData(), templateName = "UsersReport"});
        }


    }
}