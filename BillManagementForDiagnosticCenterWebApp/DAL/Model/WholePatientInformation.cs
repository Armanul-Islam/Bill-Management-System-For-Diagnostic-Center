using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillManagementForDiagnosticCenterWebApp.DAL.Model
{
    public class WholePatientInformation
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MobileNumber { get; set; }
        public double TotalFee { get; set; }
        public string BillNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string TestName { get; set; }
        public double TestFee { get; set; }
        public string TestTypeName { get; set; }
    }
}