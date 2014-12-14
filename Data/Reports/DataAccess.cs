using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMTH.Data.Reports
{
    public class ReportLog
    {
        Guid? ReportId { get; set; }
        String ReportName { get; set; }
        DateTime PrintTime { get; set; }
    }

    public class DataAccess
    {
        
    }
}