using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMTH.Data
{
    public enum RoleE
    {
        Administrator = 0,
        Manager = 1,
        Accountant = 2,
        Driver = 3,
        Storekeeper = 4
    }

    public enum PermissionE
    {
        //Users
        RegisterUser,
        UserPermissions,
        //Warehouse

        //Logistics

        //Sales

        //Reports
        ReportsReport,
        UsersReport,
        ProductionReport,
        LogisticsReport,
        ProfitReport
    }
}