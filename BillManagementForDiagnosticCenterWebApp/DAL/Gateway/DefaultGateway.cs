using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace BillManagementForDiagnosticCenterWebApp.DAL.Gateway
{
    public class DefaultGateway
    {
        private string connectionString =
            WebConfigurationManager.ConnectionStrings["BillManagementForDiagnosticCenterDB"].ConnectionString;

        public SqlConnection Connection { get; set; }
        public SqlCommand Command { get; set; }
        public SqlDataReader Reader { get; set; }
        public string Query { get; set; }

        public DefaultGateway()
        {
            Connection=new SqlConnection(connectionString);
        }
    }
}