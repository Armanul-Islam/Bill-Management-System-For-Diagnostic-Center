using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillManagementForDiagnosticCenterWebApp.DAL.Gateway;
using BillManagementForDiagnosticCenterWebApp.DAL.Model;

namespace BillManagementForDiagnosticCenterWebApp.BLL
{
    public class TestManager
    {
        TestGateway aTestGateway = new TestGateway();
        public string Save(Test aTest)
        {
            if (aTest.TestName == String.Empty || aTest.Fee == String.Empty)
            {
                return "Please Enter Test Name and Fee";
            }
            else
            {
                if (Convert.ToDouble(aTest.Fee) < 0)
                {
                    return "Fee Can't Be Negative!";
                }
                else
                {
                    if (aTestGateway.DoesTestNameExist(aTest))
                    {
                        return "Test Name Exists Already!";
                    }
                    else
                    {
                        return aTestGateway.Save(aTest);
                    }
                }
            }
        }

        public List<Test> GetAllTests()
        {
            return aTestGateway.GetAllTests();
        }

        public Test GetTestById(int testId)
        {
            return aTestGateway.GetTestById(testId);
        }
    }
}