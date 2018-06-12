using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BillManagementForDiagnosticCenterWebApp.BLL;
using BillManagementForDiagnosticCenterWebApp.DAL.Model;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace BillManagementForDiagnosticCenterWebApp.UI
{
    public partial class TypeWiseReportUI : System.Web.UI.Page
    {
        ReportManager aReportManager = new ReportManager();
        TestTypeManager aTestTypeManager=new TestTypeManager();
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
                {
                    Report aReport = new Report();
                    aReport.FromDate = Convert.ToDateTime(FromDateTextBox.Text);
                    aReport.ToDate = Convert.ToDateTime(ToDateTextBox.Text);
                    List<WholePatientInformation> wholePatientInformations = aReportManager.GetPatientInfoWithinInterval(aReport);
                    if (wholePatientInformations.Count == 0)
                    {
                        MessageLabel.Text = "No type of test was done in this interval!";
                        ClearGridView();
                    }
                    else
                    {
                        MessageLabel.Text = String.Empty;
                        List<TestType> testTypes = aTestTypeManager.GetAllTestTypes();
                        List<TestAndTypeWiseReport> testAndTypeWiseReports = new List<TestAndTypeWiseReport>();
                        double totalAmount = 0;
                        foreach (TestType aTestType in testTypes)
                        {
                            TestAndTypeWiseReport aTestAndTypeWiseReport = new TestAndTypeWiseReport();
                            aTestAndTypeWiseReport.TypeName = aTestType.TypeName;
                            aTestAndTypeWiseReport.TotalTest = 0;
                            aTestAndTypeWiseReport.TotalAmount = 0;
                            foreach (WholePatientInformation aWholePatientInformation in wholePatientInformations)
                            {
                                if (aTestType.TypeName == aWholePatientInformation.TestTypeName)
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
                        ShowTypeWiseReportGridView.DataSource = testAndTypeWiseReports;
                        ShowTypeWiseReportGridView.DataBind();
                        ShowTypeWiseReportGridView.RowStyle.Height = 35;
                        TotalAmountTextBox.Text = totalAmount.ToString();
                        pdfButton.Visible = true;
                    }
                }
            }
        }

        private void ClearGridView()
        {
            ShowTypeWiseReportGridView.DataSource = null;
            ShowTypeWiseReportGridView.DataBind();
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
            Response.AddHeader("content-disposition", "attachment;filename=Type Wise Report.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            ShowTypeWiseReportGridView.RenderControl(hw);
            ShowTypeWiseReportGridView.HeaderRow.Style.Add("width", "15%");
            ShowTypeWiseReportGridView.HeaderRow.Style.Add("font-size", "10px");
            ShowTypeWiseReportGridView.Style.Add("text-decoration", "none");
            ShowTypeWiseReportGridView.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
            ShowTypeWiseReportGridView.Style.Add("font-size", "8px");
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
            ShowTypeWiseReportGridView.AllowPaging = true;
            ShowTypeWiseReportGridView.DataBind();
        }
    }
}