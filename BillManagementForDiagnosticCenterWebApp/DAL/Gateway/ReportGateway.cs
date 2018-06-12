using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BillManagementForDiagnosticCenterWebApp.DAL.Model;

namespace BillManagementForDiagnosticCenterWebApp.DAL.Gateway
{
    public class ReportGateway : DefaultGateway
    {
        public List<WholePatientInformation> GetPatientInfoWithinInterval(Report aReport)
        {
            List<WholePatientInformation> wholePatientInformations=new List<WholePatientInformation>();
            
            Query = "SELECT * FROM AllPatientsAndTestsWithType WHERE InvoiceDate BETWEEN @fromDate AND @toDate";
            Command=new SqlCommand(Query,Connection);
            Command.Parameters.AddWithValue("fromDate", aReport.FromDate);
            Command.Parameters.AddWithValue("toDate", aReport.ToDate);
            Connection.Open();
            Reader = Command.ExecuteReader();
            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    WholePatientInformation aWholePatientInformation = new WholePatientInformation();
                    aWholePatientInformation.Id = Convert.ToInt32(Reader["Id"]);
                    aWholePatientInformation.PatientName = Reader["PatientName"].ToString();
                    aWholePatientInformation.DateOfBirth = Convert.ToDateTime(Reader["DateOfBirth"]);
                    aWholePatientInformation.MobileNumber = Reader["MobileNumber"].ToString();
                    aWholePatientInformation.TotalFee = Convert.ToDouble(Reader["TotalFee"]);
                    aWholePatientInformation.BillNumber = Reader["BillNumber"].ToString();
                    aWholePatientInformation.InvoiceDate = Convert.ToDateTime(Reader["InvoiceDate"]);
                    aWholePatientInformation.TestName = Reader["TestName"].ToString();
                    aWholePatientInformation.TestFee = Convert.ToDouble(Reader["Fee"]);
                    aWholePatientInformation.TestTypeName = Reader["TestTypeName"].ToString();
                    wholePatientInformations.Add(aWholePatientInformation);
                }
                Reader.Close();
                Connection.Close();
                
            }
            else
            {
                Reader.Close();
                Connection.Close();
            }
            return wholePatientInformations;
        }
    }
}