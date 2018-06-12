using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillManagementForDiagnosticCenterWebApp.DAL.Model
{
    [Serializable]
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MobileNumber { get; set; }
        public double Fee { get; set; }
        public string BillNumber { get; set; }
        public int PaymrentStatus { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}