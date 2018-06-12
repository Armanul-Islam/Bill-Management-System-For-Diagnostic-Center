using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BillManagementForDiagnosticCenterWebApp.DAL.Model;

namespace BillManagementForDiagnosticCenterWebApp.DAL.Gateway
{
    public class TestGateway : DefaultGateway
    {
        public string Save(Test aTest)
        {
            Query = "INSERT INTO Test (TestName,Fee,TypeId) VALUES (@testName,@fee,@typeId)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("testName", aTest.TestName);
            Command.Parameters.AddWithValue("fee", aTest.Fee);
            Command.Parameters.AddWithValue("typeId", aTest.TypeId);
            Connection.Open();
            int rowsAffected = Command.ExecuteNonQuery();
            Connection.Close();
            if (rowsAffected > 0)
            {
                return "Test Saved Successfully";
            }
            else
            {
                return "Failed to Save!";
            }
        }

        public bool DoesTestNameExist(Test aTest)
        {
            Query = "SELECT DISTINCT TestName FROM Test WHERE TestName=@testName";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("testName", aTest.TestName);
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

        public List<Test> GetAllTests()
        {
            List<Test> tests = new List<Test>();
            Query = "SELECT * FROM TestWiseType ORDER BY TestName ASC";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Test aTest = new Test();
                aTest.Id = Convert.ToInt32(Reader["TestId"]);
                aTest.TestName = Reader["TestName"].ToString();
                aTest.Fee = Reader["Fee"].ToString();
                aTest.TestTypeName = Reader["TestTypeName"].ToString();
                tests.Add(aTest);
            }
            Reader.Close();
            Connection.Close();
            return tests;
        }

        public Test GetTestById(int testId)
        {
            Test aTest = new Test();
            Query = "SELECT * FROM TestWiseType WHERE TestId=@testId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("testId", testId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                aTest.Id = Convert.ToInt32(Reader["TestId"]);
                aTest.TestName = Reader["TestName"].ToString();
                aTest.Fee = Reader["Fee"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return aTest;
        }
    }
}