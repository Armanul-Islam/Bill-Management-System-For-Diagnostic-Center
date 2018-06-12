<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="BillManagementForDiagnosticCenterWebApp.UI.Payment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment</title>
    <link href="../Content/Styles.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 125px;
        }
    </style>
</head>
<body>
    <div class="container">

        <header>
            <h1>Bill Management System For Diagnostic Center</h1>
        </header>

        <div class="navbar">
            <a href="IndexUI.aspx">Home</a>
            <div class="dropdown">
                <button class="dropbtn">Setup <i class="fa fa-caret-down"></i></button>
                <div class="dropdown-content">
                    <a href="TestTypeSetup.aspx">Test Type</a>
                    <a href="TestSetup.aspx">Test</a>
                </div>
            </div>
            <div class="dropdown">
                <button class="dropbtn">Test Request <i class="fa fa-caret-down"></i></button>
                <div class="dropdown-content">
                    <a href="TestRequestEntry.aspx">Entry</a>
                    <a href="Payment.aspx">Payment</a>
                </div>
            </div>
            <div class="dropdown">
                <button class="dropbtn">Report <i class="fa fa-caret-down"></i></button>
                <div class="dropdown-content">
                    <a href="TestWiseReport.aspx">Test Wise</a>
                    <a href="TypeWiseReportUI.aspx">Type Wise</a>
                    <a href="UnpaidBillReportUI.aspx">Unpaid Bill</a>
                </div>
            </div>
        </div>
        <div class="content">
            <form id="form1" runat="server">
                <div>
                    <fieldset style="width: 442px; height: 337px">
                        <legend>Payment</legend>
                        <div>
                            <fieldset style="height: 145px">
                                <legend></legend>
                                <div>
                                    <table>
                                        <tr>
                                            <th class="auto-style1">Bill Number</th>
                                            <td>
                                                <asp:TextBox ID="BillNumberTextBox" runat="server" Height="28px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th class="auto-style1">Mobile Number</th>
                                            <td>
                                                <asp:TextBox ID="MobileNumberTextBox" runat="server" Height="28px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" Width="70px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Label ID="MessageLabel" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </fieldset>
                            <fieldset style="height: 145px">
                                <legend></legend>
                                <div>
                                    <table>
                                        <tr>
                                            <th>Amount</th>
                                            <td>
                                                <asp:TextBox ID="AmountTextBox" ReadOnly="True" runat="server" Height="28px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>Due Date</th>
                                            <td>
                                                <asp:TextBox ID="DueDateTextBox" ReadOnly="True" runat="server" Height="28px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="PayButton" runat="server" Text="Pay" Width="69px" OnClick="PayButton_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Label ID="ConfirmationLabel" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </fieldset>
                        </div>
                    </fieldset>
                </div>
            </form>
        </div>

        <footer>Copyright &copy; ArmanSheikh</footer>

    </div>
</body>
</html>
