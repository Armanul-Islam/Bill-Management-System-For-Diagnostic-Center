using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BillManagementForDiagnosticCenterWebApp.DAL.Model;

namespace BillManagementForDiagnosticCenterWebApp.DAL.Gateway
{
    public class TestTypeGateway : DefaultGateway
    {
        public string Save(TestType aTestType)
        {
            Query = "INSERT INTO TestType (TestTypeName) VALUES (@testType)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("testType", aTestType.TypeName);
            Connection.Open();
            int rowsAffected = Command.ExecuteNonQuery();
            Connection.Close();
            if (rowsAffected > 0)
            {
                return "Test Type Successfully Saved";
            }
            else
            {
                return "Failed to save Test Type";
            }
        }

        public bool DoesTestTypeExist(TestType aTestType)
        {
            Query = "SELECT DISTINCT TestTypeName FROM TestType WHERE TestTypeName=@typeName";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("typeName", aTestType.TypeName);
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

        public List<TestType> GetAllTestTypes()
        {
            List<TestType> testTypes = new List<TestType>();
            Query = "SELECT * FROM TestType ORDER BY TestTypeName ASC";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                TestType aTestType = new TestType();
                aTestType.Id = (int)Reader["Id"];
                aTestType.TypeName = Reader["TestTypeName"].ToString();
                testTypes.Add(aTestType);
            }
            Reader.Close();
            Connection.Close();
            return testTypes;
        }
    }
}