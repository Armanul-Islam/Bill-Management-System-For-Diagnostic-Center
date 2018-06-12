using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BillManagementForDiagnosticCenterWebApp.BLL;
using BillManagementForDiagnosticCenterWebApp.DAL.Model;

namespace BillManagementForDiagnosticCenterWebApp.UI
{
    public partial class TestTypeSetup : System.Web.UI.Page
    {
        TestTypeManager aTestTypeManager=new TestTypeManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindDataToGridview();
        }

        protected void TypeNameButton_Click(object sender, EventArgs e)
        {
            TestType aTestType=new TestType();
            aTestType.TypeName = TypeNameTextBox.Text;
            ConfirmationLabel.Text=aTestTypeManager.Save(aTestType);
            BindDataToGridview();
        }

        private void BindDataToGridview()
        {
            ShowAllTestTypesGridView.DataSource = aTestTypeManager.GetAllTestTypes();
            ShowAllTestTypesGridView.DataBind();
            ShowAllTestTypesGridView.RowStyle.Height = 35;
        }
    }
}