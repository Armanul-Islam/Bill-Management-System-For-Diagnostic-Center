using System;
using System.Collections.Generic;
using System.EnterpriseServices;
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
    public partial class UnpaidBillReportUI : System.Web.UI.Page
    {
        PatientManager aPatientManager=new PatientManager();
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
                List<Patient> patients = aPatientManager.GetUnpaidPatientsWithinInterval(aReport);
                if (patients.Count == 0)
                {
                    MessageLabel.Text = "No unpaid bill in this interval!";
                    ClearGridView();
                }
                else
                {
                    double totalAmount = 0;
                    foreach (Patient aPatient in patients)
                    {
                        totalAmount += aPatient.Fee;
                    }
                    ShowUnpaidBillReportGridView.DataSource = patients;
                    ShowUnpaidBillReportGridView.DataBind();
                    ShowUnpaidBillReportGridView.RowStyle.Height = 35;
                    TotalAmountLabel.Visible = true;
                    TotalAmountTextBox.Visible = true;
                    TotalAmountTextBox.Text = totalAmount.ToString();
                    pdfButton.Visible = true;
                }
            }
        }

        private void ClearGridView()
        {
            ShowUnpaidBillReportGridView.DataSource = null;
            ShowUnpaidBillReportGridView.DataBind();
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
            Response.AddHeader("content-disposition", "attachment;filename=Unpaid Bill Report.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            ShowUnpaidBillReportGridView.RenderControl(hw);
            ShowUnpaidBillReportGridView.HeaderRow.Style.Add("width", "15%");
            ShowUnpaidBillReportGridView.HeaderRow.Style.Add("font-size", "10px");
            ShowUnpaidBillReportGridView.Style.Add("text-decoration", "none");
            ShowUnpaidBillReportGridView.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
            ShowUnpaidBillReportGridView.Style.Add("font-size", "8px");
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
            ShowUnpaidBillReportGridView.AllowPaging = true;
            ShowUnpaidBillReportGridView.DataBind();
        }
    }
}