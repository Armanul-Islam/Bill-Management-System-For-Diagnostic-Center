using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BillManagementForDiagnosticCenterWebApp.BLL;
using BillManagementForDiagnosticCenterWebApp.DAL.Model;

namespace BillManagementForDiagnosticCenterWebApp.UI
{
    public partial class Payment : System.Web.UI.Page
    {
        PatientManager aPatientManager = new PatientManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            PayButton.Visible = false;
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            if (BillNumberTextBox.Text == String.Empty && MobileNumberTextBox.Text == String.Empty)
            {
                MessageLabel.Text = "Please enter bill or mobile number!";
                DueDateTextBox.Text = String.Empty;
                AmountTextBox.Text = String.Empty;
            }
            else
            {
                if (BillNumberTextBox.Text == String.Empty &&
                    !new Regex(@"(^(\+8801|8801|01|008801))[1|5-9]{1}(\d){8}$").IsMatch(MobileNumberTextBox.Text))
                {
                    MessageLabel.Text = "Please enter a Valid Bill or Mobile Number!";
                    ConfirmationLabel.Text=String.Empty;
                    DueDateTextBox.Text = String.Empty;
                    AmountTextBox.Text = String.Empty;
                }
                else
                {
                    if (aPatientManager.DoesBillNumberExist(BillNumberTextBox.Text) || aPatientManager.DoesMobileNumberExist(MobileNumberTextBox.Text))
                    {
                        MessageLabel.Text=String.Empty;
                        Patient aPatient = aPatientManager.GetPatientByBillNumberOrMobileNumber(BillNumberTextBox.Text,
                        MobileNumberTextBox.Text);
                        ViewState["Patient"] = aPatient;
                        BillNumberTextBox.Text = aPatient.BillNumber;
                        MobileNumberTextBox.Text = aPatient.MobileNumber;
                        AmountTextBox.Text = aPatient.Fee.ToString();
                        DueDateTextBox.Text = aPatient.InvoiceDate.ToString("dd-MM-yyyy");
                        PayButton.Visible = true;
                        ConfirmationLabel.Text=String.Empty;
                    }
                    else
                    {
                        MessageLabel.Text = "Entered Bill Number Or Mobile Number Does Not Exist!";
                        DueDateTextBox.Text = String.Empty;
                        AmountTextBox.Text = String.Empty;
                        ConfirmationLabel.Text=String.Empty;
                    }
                }
            }
        }

        protected void PayButton_Click(object sender, EventArgs e)
        {
            Patient aPatient = (Patient)ViewState["Patient"];
            if (aPatient.Fee > 0)
            {
                if (aPatientManager.Payment(aPatient))
                {
                    ConfirmationLabel.Text = "Payment Done Successfully.";
                }
                else
                {
                    ConfirmationLabel.Text = "Failed to clear Payment!";
                }
            }
            else
            {
                ConfirmationLabel.Text = "Payment Already Done!";
            }
        }
    }
}