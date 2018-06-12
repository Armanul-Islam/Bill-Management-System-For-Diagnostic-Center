using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Web;
using System.Web.UI.WebControls;

namespace BillManagementForDiagnosticCenterWebApp.DAL.Model
{
    [Serializable]
    public class Test
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public string Fee { get; set; }
        public int TypeId { get; set; }
        public string TestTypeName { get; set; }
    }
}