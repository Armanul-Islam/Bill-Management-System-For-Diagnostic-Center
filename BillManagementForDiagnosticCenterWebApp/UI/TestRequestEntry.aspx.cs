using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BillManagementForDiagnosticCenterWebApp.BLL;
using BillManagementForDiagnosticCenterWebApp.DAL.Model;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace BillManagementForDiagnosticCenterWebApp.UI
{
    public partial class TestRequestEntry : System.Web.UI.Page
    {
        TestManager aTestManager = new TestManager();
        static List<Test> tests = new List<Test>();
        PatientManager aPatientManager=new PatientManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SelectTestDropDownList.DataSource = aTestManager.GetAllTests();
                SelectTestDropDownList.DataValueField = "Id";
                SelectTestDropDownList.DataTextField = "TestName";
                SelectTestDropDownList.DataBind();
                SelectTestDropDownList.Items.Insert(0, new ListItem("---Select a Test---", "0"));
                TotalLabel.Visible = false;
                TotalTextBox.Visible = false;
                SaveButton.Visible = false;
                tests.Clear();
            }
            AddButton.Visible = false;
        }

        protected void SelectTestDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((SelectTestDropDownList.SelectedIndex == 0))
            {
                ConfirmationLabel.Text = "Please select a test!";
                FeeTextBox.Text = String.Empty;
            }
            else
            {
                int testId = Convert.ToInt32(SelectTestDropDownList.SelectedValue);
                Test aTest = aTestManager.GetTestById(testId);
                FeeTextBox.Text = aTest.Fee;
                ViewState["Test"] = aTest;
                AddButton.Visible = true;
                ConfirmationLabel.Text = String.Empty;
            }
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            if (NameOfPatientTextBox.Text == String.Empty || MobileNumberTextBox.Text == String.Empty || DateOfBirthTextBox.Text == String.Empty)
            {
                ConfirmationLabel.Text = "Please fill up all the fields";
                FeeTextBox.Text=String.Empty;
                ResetDropdown();
            }
            else
            {
                Test aTest = (Test)ViewState["Test"];
                tests.Add(aTest);
                ShowTestsGridView.DataSource = tests;
                ShowTestsGridView.DataBind();
                ShowTestsGridView.RowStyle.Height = 35;
                double sum = 0;
                foreach (Test test in tests)
                {
                    sum += Convert.ToDouble(test.Fee);
                }
                TotalTextBox.Text = sum.ToString();
                TotalLabel.Visible = true;
                TotalTextBox.Visible = true;
                SaveButton.Visible = true;
                ConfirmationLabel.Text = String.Empty;
                FeeTextBox.Text = String.Empty;
                SelectTestDropDownList.Items.Remove(SelectTestDropDownList.SelectedItem);
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (NameOfPatientTextBox.Text==String.Empty || MobileNumberTextBox.Text==String.Empty || DateOfBirthTextBox.Text==String.Empty)
            {
                MessageLabel.Text = "Please fill up all the fields";
            }
            else
            {
                if (new Regex(@"(^(\+8801|8801|01|008801))[1|5-9]{1}(\d){8}$").IsMatch(MobileNumberTextBox.Text))
                {
                    if (aPatientManager.DoesMobileNumberExist(MobileNumberTextBox.Text))
                    {
                        MessageLabel.Text = "Mobile Number Exists Already!";
                    }
                    else
                    {
                        Patient aPatient = new Patient();
                        aPatient.Name = NameOfPatientTextBox.Text;
                        aPatient.DateOfBirth = Convert.ToDateTime(DateOfBirthTextBox.Text);
                        aPatient.MobileNumber = MobileNumberTextBox.Text;
                        aPatient.Fee = Convert.ToDouble(TotalTextBox.Text);
                        aPatient.BillNumber = DateTime.Now.ToString("F").GetHashCode().ToString("x"); ;
                        aPatient.InvoiceDate = DateTime.Now.Date;
                        if (aPatientManager.SavePatient(aPatient))
                        {
                            MessageLabel.Text=String.Empty;
                            Patient patient1 = aPatientManager.GetPatientByBillNumberOrMobileNumber(aPatient.BillNumber,
                                aPatient.MobileNumber);
                            aPatient.Id = patient1.Id;
                            ViewState["patient"] = patient1;
                            foreach (Test aTest in tests)
                            {
                                aPatientManager.SavePatientAndTests(aTest, aPatient.Id);
                            }
                            GeneratePdf();
                            MessageLabel.Text = "Successfully saved patient and tests.";
                            SaveButton.Visible = false;
                        }
                        else
                        {
                            MessageLabel.Text = "Failed to save patient";
                        }
                    }
                }
                else
                {
                    MessageLabel.Text = "Please enter a valid mobile number!";
                    ResetDropdown();
                    FeeTextBox.Text=String.Empty;
                }
            }
        }

        private void ResetDropdown()
        {
            SelectTestDropDownList.Items[SelectTestDropDownList.SelectedIndex].Selected = false;
            SelectTestDropDownList.Items[0].Selected = true;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private void GeneratePdf()
        {
            Patient aPatient = (Patient)ViewState["patient"];
            string printDate = DateTime.Now.ToString("F");
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename="+aPatient.MobileNumber+".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            ShowTestsGridView.RenderControl(hw);
            ShowTestsGridView.HeaderRow.Style.Add("width", "15%");
            ShowTestsGridView.HeaderRow.Style.Add("font-size", "10px");
            ShowTestsGridView.Style.Add("text-decoration", "none");
            ShowTestsGridView.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
            ShowTestsGridView.Style.Add("font-size", "8px");
            Paragraph paragraph1=new Paragraph("XXX Diagnostic Center\n\n");
            paragraph1.Font.Size = 30;
            Paragraph paragraph2=new Paragraph("Print Date: "+printDate+"\n\n");
            Paragraph paragraph3 = new Paragraph(" Bill Number: " + aPatient.BillNumber + "\n Invoice Date: " + aPatient.InvoiceDate.ToString("d") + "\n Patient Name: " + aPatient.Name + "\n Date of Birth: " + aPatient.DateOfBirth.ToString("d") + "\n Mobile Number: " + aPatient.MobileNumber + "\n\n");
            Paragraph paragraph4=new Paragraph("Total Fee: " + aPatient.Fee + "");
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.Add(paragraph1);
            pdfDoc.Add(paragraph2);
            pdfDoc.Add(paragraph3);
            htmlparser.Parse(sr);
            pdfDoc.Add(paragraph4);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
            ShowTestsGridView.AllowPaging = true;
            ShowTestsGridView.DataBind();
        }
    }
}