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
    public partial class TestSetup : System.Web.UI.Page
    {
        TestTypeManager aTestTypeManager=new TestTypeManager();
        TestManager aTestManager=new TestManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TestTypeDropDownList.DataSource = aTestTypeManager.GetAllTestTypes();
                TestTypeDropDownList.DataTextField = "TypeName";
                TestTypeDropDownList.DataValueField = "Id";
                TestTypeDropDownList.DataBind();
                TestTypeDropDownList.Items.Insert(0, new ListItem("---Select a Test Type---", "0"));
            }
            BindDataToGridview();
        }
        protected void TestNameButton_Click(object sender, EventArgs e)
        {
            if (TestTypeDropDownList.SelectedIndex == 0)
            {
                ConfirmationLabel.Text = "Please select a test type!";
            }
            else
            {
                Test aTest = new Test();
                aTest.TestName = TestNameTextBox.Text;
                aTest.Fee = (FeeTextBox.Text);
                aTest.TypeId = Convert.ToInt32(TestTypeDropDownList.SelectedValue);
                ConfirmationLabel.Text = aTestManager.Save(aTest);
                BindDataToGridview();
            }
            
        }
        private void BindDataToGridview()
        {
            ShowAllTestsGridView.DataSource = aTestManager.GetAllTests();
            ShowAllTestsGridView.DataBind();
            ShowAllTestsGridView.RowStyle.Height = 35;
        }
    }
}