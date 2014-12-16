using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMTH.Data.Users;

namespace SMTH.Data.Reports
{
    public class IncomingDto
    {
        public string templateName { get; set; }
        public object data { get; set; }
    }

    public class UsersReportDto
    {
        public List<UserReportRow> Rows {get;set;}
    }

    public class UserReportRow
    {
        public String UserName {get;set;}
        public String RoleName {get;set;}
    }

    public class ReportsReportDto
    {
        public List<ReportsReportRow> Rows { get; set; }
    }

    public class ReportsReportRow
    {
        public String ReportName { get; set; }
        public String UserName { get; set; }
        public String PrintTime { get; set; }
    }

    public class ReportsReportRowDT
    {
        public String ReportName { get; set; }
        public String UserName { get; set; }
        public DateTime PrintTime { get; set; }
    }

    public class DataAccess
    {
        public static void LogReport(Guid userId, String reportName)
        {
            using (var db = new UsersViewDataContext())
            {
                db.ReportPrints.InsertOnSubmit(new ReportPrint
                {
                    ReportPrintUuid = Guid.NewGuid(),
                    ReportUuid = (from r in db.Reports where r.ReportName == reportName select r.ReportUuid).FirstOrDefault(),
                    PrintTime = DateTime.Now,
                    UserUuid = userId
                });
                db.SubmitChanges();
            }
        }

        public static UsersReportDto GetUsersReportData()
        {
            using(var db = new UsersViewDataContext())
            {
                var rList = (from u in db.Users
                             join r in db.Roles on u.UserRoleUuid equals r.RoleUuid
                             select new UserReportRow
                             {
                                 UserName = u.UserName,
                                 RoleName = r.Rolename
                             }).ToList();
                return new UsersReportDto { Rows = rList };
            }
        }

        public static ReportsReportDto GetReportsReportData(DateTime startDate, DateTime endDate)
        {
            using (var db = new UsersViewDataContext())
            {
                var rList = (from u in db.Users
                             join rp in db.ReportPrints on u.UserUuid equals  rp.UserUuid
                             join r in db.Reports on rp.ReportUuid equals r.ReportUuid
                             where rp.PrintTime >= startDate
                             where rp.PrintTime <= endDate.AddDays(1)
                             select new ReportsReportRowDT
                             {
                                 PrintTime = rp.PrintTime,
                                 UserName = u.UserName,
                                 ReportName = r.ReportName
                             }
                             ).ToList();

                var retList = new List<ReportsReportRow>();
                foreach(var r in rList)
                {
                    retList.Add(new ReportsReportRow {
                        PrintTime = r.PrintTime.ToString("yyyy-MM-dd HH:mm"),
                        UserName = r.UserName,
                        ReportName = r.ReportName
                    });
                }
                return new ReportsReportDto { Rows = retList };
            }
        }
    }
}