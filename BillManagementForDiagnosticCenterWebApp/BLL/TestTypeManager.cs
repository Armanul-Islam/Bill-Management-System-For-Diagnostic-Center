using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillManagementForDiagnosticCenterWebApp.DAL.Gateway;
using BillManagementForDiagnosticCenterWebApp.DAL.Model;

namespace BillManagementForDiagnosticCenterWebApp.BLL
{
    public class TestTypeManager
    {
        TestTypeGateway aTestTypeGateway = new TestTypeGateway();
        public string Save(TestType aTestType)
        {
            if (aTestType.TypeName==String.Empty)
            {
                return "Please Enter a Type of Test";
            }
            else
            {
                if (aTestTypeGateway.DoesTestTypeExist(aTestType))
                {
                    return "Test Type Already Exists";
                }
                else
                {
                    return aTestTypeGateway.Save(aTestType);
                }
            }
            
        }

        public List<TestType> GetAllTestTypes()
        {
            return aTestTypeGateway.GetAllTestTypes();
        }
    }
}