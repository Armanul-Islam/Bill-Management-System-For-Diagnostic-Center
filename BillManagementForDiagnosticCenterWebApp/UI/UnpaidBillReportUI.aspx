<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnpaidBillReportUI.aspx.cs" Inherits="BillManagementForDiagnosticCenterWebApp.UI.UnpaidBillReportUI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Unpaid Bill Report</title>
    <link href="../Content/bootstrap-datepicker.css" rel="stylesheet" />
    <link href="../Content/Styles.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 200px;
            text-align: right;
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
            <form id="form2" runat="server">
                <div>
                    <fieldset style="width: 560px">
                        <legend>Unpaid Bill Report</legend>
                        <div>
                            <table>
                                <tr>
                                    <th>From Date</th>
                                    <td>
                                        <asp:TextBox ID="FromDateTextBox" runat="server" Height="25px" Width="169px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>To Date</th>
                                    <td>
                                        <asp:TextBox ID="ToDateTextBox" runat="server" Height="25px" Width="169px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="ShowButton" runat="server" Text="Show" Height="25px" Width="58px" OnClick="ShowButton_Click"/>
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

                    <asp:GridView ID="ShowUnpaidBillReportGridView" runat="server" AutoGenerateColumns="False" Width="450px" style="margin-left: 5px">
                        <Columns>
                            <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                    <span>
                                        <%#Container.DataItemIndex + 1%>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill Number">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("BillNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact Number">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("MobileNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Patient Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill Amount">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("Fee") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="pdfButton" runat="server" Text="PDF" OnClick="pdfButton_Click" Width="70px" />
                            </td>
                            <th class="auto-style1">
                                <asp:Label ID="TotalAmountLabel" runat="server" Text="Total"></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="TotalAmountTextBox" ReadOnly="True" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </form>
        </div>

        <footer>Copyright &copy; ArmanSheikh</footer>

    </div>
    <script src="../Scripts/jquery-3.3.1.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap-datepicker.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#FromDateTextBox').datepicker({
                format: "dd-mm-yyyy",
                autoclose: true,
                todayHighlight: true,
                endDate: new Date()
            }).on('changeDate', function (selected) {
                var minDate = new Date(selected.date.valueOf());
                $('#ToDateTextBox').datepicker('setStartDate', minDate);
            });
            $('#ToDateTextBox').datepicker({
                format: "dd-mm-yyyy",
                autoclose: true,
                todayHighlight: true,
                endDate: new Date()
            });
        });
    </script>
</body>
</html>
