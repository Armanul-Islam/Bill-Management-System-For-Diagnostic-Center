using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillManagementForDiagnosticCenterWebApp.DAL.Model
{
    public class Report
    {
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}