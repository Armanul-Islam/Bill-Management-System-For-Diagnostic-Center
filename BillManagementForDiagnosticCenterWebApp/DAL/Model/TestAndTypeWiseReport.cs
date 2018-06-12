using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillManagementForDiagnosticCenterWebApp.DAL.Model
{
    public class TestAndTypeWiseReport
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public string TypeName { get; set; }
        public int TotalTest { get; set; }
        public double TotalAmount { get; set; }
    }
}