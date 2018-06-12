using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BillManagementForDiagnosticCenterWebApp.DAL.Model;

namespace BillManagementForDiagnosticCenterWebApp.DAL.Gateway
{
    public class PatientGateway : DefaultGateway
    {
        public bool SavePatient(Patient aPatient)
        {
            Query = "INSERT INTO Patient (PatientName,DateOfBirth,MobileNumber,TotalFee,BillNumber,PaymentStatus,InvoiceDate) VALUES (@patientName,@dateOfBirth,@mobileNumber,@totalFee,@billNumber,@paymentStatus,@invoiceDate)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("patientName", aPatient.Name);
            Command.Parameters.AddWithValue("dateOfBirth", aPatient.DateOfBirth);
            Command.Parameters.AddWithValue("mobileNumber", aPatient.MobileNumber);
            Command.Parameters.AddWithValue("totalFee", aPatient.Fee);
            Command.Parameters.AddWithValue("billNumber", aPatient.BillNumber);
            Command.Parameters.AddWithValue("paymentStatus", 0);
            Command.Parameters.AddWithValue("invoiceDate", aPatient.InvoiceDate);
            Connection.Open();
            int rowsAffected = Command.ExecuteNonQuery();
            Connection.Close();
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SavePatientAndTests(Test aTest, int patientId)
        {
            Query = "INSERT INTO PatientAndTests (PatientId,TestId) VALUES (@patientId,@testId)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("patientId", patientId);
            Command.Parameters.AddWithValue("testId", aTest.Id);
            Connection.Open();
            Command.ExecuteNonQuery();
            Connection.Close();
        }

        public Patient GetPatientByBillNumberOrMobileNumber(string billNumber, string mobileNumber)
        {
            Patient aPatient = new Patient();
            Query = "SELECT * FROM Patient WHERE MobileNumber=@mobileNumber OR BillNumber=@billNumber";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("mobileNumber", mobileNumber);
            Command.Parameters.AddWithValue("billNumber", billNumber);
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                aPatient.Id = Convert.ToInt32(Reader["Id"]);
                aPatient.Name = Reader["PatientName"].ToString();
                aPatient.DateOfBirth = Convert.ToDateTime(Reader["DateOfBirth"]);
                aPatient.MobileNumber = Reader["MobileNumber"].ToString();
                aPatient.Fee = Convert.ToDouble(Reader["TotalFee"]);
                aPatient.BillNumber = Reader["BillNumber"].ToString();
                aPatient.InvoiceDate = Convert.ToDateTime(Reader["InvoiceDate"]);
            }
            Reader.Close();
            Connection.Close();
            return aPatient;
        }

        public bool DoesMobileNumberExist(string mobileNumber)
        {
            Query = "SELECT MobileNumber FROM Patient WHERE MobileNumber=@mobileNumber";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("mobileNumber", mobileNumber);
            Connection.Open();
            Reader = Command.ExecuteReader();
            if (Reader.HasRows)
            {
                bool hasRows = Reader.HasRows;
                Reader.Close();
                Connection.Close();
                return hasRows;
            }
            else
            {
                Reader.Close();
                Connection.Close();
                return false;
            }
        }

        public bool DoesBillNumberExist(string billNumber)
        {
            Query = "SELECT BillNumber FROM Patient WHERE BillNumber=@billNumber";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("billNumber", billNumber);
            Connection.Open();
            Reader = Command.ExecuteReader();
            if (Reader.HasRows)
            {
                bool hasRows = Reader.HasRows;
                Reader.Close();
                Connection.Close();
                return hasRows;
            }
            else
            {
                Reader.Close();
                Connection.Close();
                return false;
            }
        }

        public bool Payment(Patient aPatient)
        {
            Query = "UPDATE Patient SET PaymentStatus = @paymentStatus, TotalFee= @totalFee WHERE Id =@id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("paymentStatus", "TRUE");
            Command.Parameters.AddWithValue("totalFee", aPatient.Fee-aPatient.Fee);
            Command.Parameters.AddWithValue("id", aPatient.Id);
            Connection.Open();
            int rowsAffected = Command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Patient> GetUnpaidPatientsWithinInterval(Report aReport)
        {
            List<Patient> patients=new List<Patient>();
            Query = "SELECT * FROM Patient WHERE InvoiceDate BETWEEN @fromDate AND @toDate AND PaymentStatus=@paymentStatus";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("fromDate", aReport.FromDate);
            Command.Parameters.AddWithValue("toDate", aReport.ToDate);
            Command.Parameters.AddWithValue("paymentStatus", "FALSE");
            Connection.Open();
            Reader = Command.ExecuteReader();
            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    Patient aPatient = new Patient();
                    aPatient.Id = Convert.ToInt32(Reader["Id"]);
                    aPatient.Name = Reader["PatientName"].ToString();
                    aPatient.DateOfBirth = Convert.ToDateTime(Reader["DateOfBirth"]);
                    aPatient.MobileNumber = Reader["MobileNumber"].ToString();
                    aPatient.Fee = Convert.ToDouble(Reader["TotalFee"]);
                    aPatient.BillNumber = Reader["BillNumber"].ToString();
                    aPatient.InvoiceDate = Convert.ToDateTime(Reader["InvoiceDate"]);
                    patients.Add(aPatient);
                }
                Reader.Close();
                Connection.Close();
            }
            else
            {
                Reader.Close();
                Connection.Close();
            }
            return patients;
        }
    }
}