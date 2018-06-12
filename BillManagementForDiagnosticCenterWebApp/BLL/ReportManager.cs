using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillManagementForDiagnosticCenterWebApp.DAL.Gateway;
using BillManagementForDiagnosticCenterWebApp.DAL.Model;

namespace BillManagementForDiagnosticCenterWebApp.BLL
{
    public class ReportManager
    {
        ReportGateway aReportGateway=new ReportGateway();
        public List<WholePatientInformation> GetPatientInfoWithinInterval(Report aReport)
        {
            return aReportGateway.GetPatientInfoWithinInterval(aReport);
        }
    }
}