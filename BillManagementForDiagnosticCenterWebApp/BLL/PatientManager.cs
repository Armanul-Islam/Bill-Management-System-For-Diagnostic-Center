using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillManagementForDiagnosticCenterWebApp.DAL.Gateway;
using BillManagementForDiagnosticCenterWebApp.DAL.Model;

namespace BillManagementForDiagnosticCenterWebApp.BLL
{
    public class PatientManager
    {
        PatientGateway aPatientGateway=new PatientGateway();
        public bool SavePatient(Patient aPatient)
        {
            return aPatientGateway.SavePatient(aPatient);
        }

        public void SavePatientAndTests(Test aTest,int patientId)
        {
            aPatientGateway.SavePatientAndTests(aTest,patientId);
        }

        public Patient GetPatientByBillNumberOrMobileNumber(string billNumber, string mobileNumber)
        {
            return aPatientGateway.GetPatientByBillNumberOrMobileNumber(billNumber, mobileNumber);
        }

        public bool DoesMobileNumberExist(string mobileNumber)
        {
            return aPatientGateway.DoesMobileNumberExist(mobileNumber);
        }
        public bool DoesBillNumberExist(string billNumber)
        {
            return aPatientGateway.DoesBillNumberExist(billNumber);
        }

        public bool Payment(Patient aPatient)
        {
            return aPatientGateway.Payment(aPatient);
        }

        public List<Patient> GetUnpaidPatientsWithinInterval(Report aReport)
        {
            return aPatientGateway.GetUnpaidPatientsWithinInterval(aReport);
        }
    }
}