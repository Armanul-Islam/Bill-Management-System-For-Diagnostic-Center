using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BillManagementForDiagnosticCenterWebApp.BLL;
using BillManagementForDiagnosticCenterWebApp.DAL.Gateway;
using BillManagementForDiagnosticCenterWebApp.DAL.Model;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace BillManagementForDiagnosticCenterWebApp.UI
{
    public partial class TestWiseReport : System.Web.UI.Page
    {
        ReportManager aReportManager=new ReportManager();
        TestManager aTestManager=new TestManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            TotalAmountLabel.Visible = false;
            TotalAmountTextBox.Visible = false;
            pdfButton.Visible = false;
        }

        protected void ShowButton_Click(object sender, EventArgs e)
        {
            if (FromDateTextBox.Text == String.Empty || ToDateTextBox.Text == String.Empty)
            {
                MessageLabel.Text = "Please select both from and to date!";
                ClearGridView();
            }
            else
            {
                Report aReport = new Report();
                aReport.FromDate = Convert.ToDateTime(FromDateTextBox.Text);
                aReport.ToDate = Convert.ToDateTime(ToDateTextBox.Text);
                List<WholePatientInformation> wholePatientInformations = aReportManager.GetPatientInfoWithinInterval(aReport);
                if (wholePatientInformations.Count == 0)
                {
                    MessageLabel.Text = "No tests was done in this interval!";
                    ClearGridView();
                }
                else
                {
                    MessageLabel.Text=String.Empty;
                    List<Test> tests = aTestManager.GetAllTests();
                    List<TestAndTypeWiseReport> testAndTypeWiseReports = new List<TestAndTypeWiseReport>();
                    double totalAmount = 0;
                    foreach (Test aTest in tests)
                    {
                        TestAndTypeWiseReport aTestAndTypeWiseReport=new TestAndTypeWiseReport();
                        aTestAndTypeWiseReport.TestName = aTest.TestName;
                        aTestAndTypeWiseReport.TotalTest = 0;
                        aTestAndTypeWiseReport.TotalAmount = 0;
                        foreach (WholePatientInformation aWholePatientInformation in wholePatientInformations)
                        {
                            if (aTest.TestName == aWholePatientInformation.TestName)
                            {
                                aTestAndTypeWiseReport.TotalTest += 1;
                                aTestAndTypeWiseReport.TotalAmount += aWholePatientInformation.TestFee;
                                totalAmount += aWholePatientInformation.TestFee;
                            }
                        }
                        testAndTypeWiseReports.Add(aTestAndTypeWiseReport);
                    }
                    TotalAmountLabel.Visible = true;
                    TotalAmountTextBox.Visible = true;
                    pdfButton.Visible = true;
                    ShowTestWiseReportGridView.DataSource = testAndTypeWiseReports;
                    ShowTestWiseReportGridView.DataBind();
                    ShowTestWiseReportGridView.RowStyle.Height = 35;
                    TotalAmountTextBox.Text = totalAmount.ToString();
                }
            }
            
        }

        private void ClearGridView()
        {
            ShowTestWiseReportGridView.DataSource = null;
            ShowTestWiseReportGridView.DataBind();
        }

        protected void pdfButton_Click(object sender, EventArgs e)
        {
            GeneratePdf();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private void GeneratePdf()
        {
            string printDate = DateTime.Now.ToString("F");
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Test Wise Report.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            ShowTestWiseReportGridView.RenderControl(hw);
            ShowTestWiseReportGridView.HeaderRow.Style.Add("width", "15%");
            ShowTestWiseReportGridView.HeaderRow.Style.Add("font-size", "10px");
            ShowTestWiseReportGridView.Style.Add("text-decoration", "none");
            ShowTestWiseReportGridView.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
            ShowTestWiseReportGridView.Style.Add("font-size", "8px");
            Paragraph paragraph1 = new Paragraph("XXX Diagnostic Center\n\n");
            paragraph1.Font.Size = 30;
            Paragraph paragraph2 = new Paragraph("Print Date: " + printDate + "\n\n");
            Paragraph paragraph3 = new Paragraph("Total " + TotalAmountTextBox.Text + "");
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.Add(paragraph1);
            pdfDoc.Add(paragraph2);
            htmlparser.Parse(sr);
            pdfDoc.Add(paragraph3);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
            ShowTestWiseReportGridView.AllowPaging = true;
            ShowTestWiseReportGridView.DataBind();
        }
    }
}