<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestRequestEntry.aspx.cs" Inherits="BillManagementForDiagnosticCenterWebApp.UI.TestRequestEntry" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test Request Entry</title>
    <link href="../Content/Styles.css" rel="stylesheet"/>
    <%--<link href="../Content/bootstrap.css" rel="stylesheet" />--%>
    <link href="../Content/bootstrap-datepicker.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 301px;
            text-align: right;
        }

        .auto-style2 {
            width: 11px;
        }

        #form1 {
            margin-top: 10px;
            margin-left: 10px;
        }

        .nameOfProperty {
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
            <form id="form1" runat="server">
                <div>
                    <fieldset style="width: 560px">
                        <legend>Test Request Entry</legend>
                        <div>
                            <table>
                                <tr>
                                    <th class="nameOfProperty">Name of the Patient</th>
                                    <td>
                                        <asp:TextBox ID="NameOfPatientTextBox" runat="server" Width="223px" Height="27px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="nameOfProperty">Date of Birth</th>
                                    <td>
                                        <asp:TextBox ID="DateOfBirthTextBox" runat="server" Width="223px" Height="27px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="nameOfProperty">Mobile Number</th>
                                    <td>
                                        <asp:TextBox ID="MobileNumberTextBox" TextMode="Number" runat="server" Width="223px" Height="27px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="nameOfProperty">Select Test</th>
                                    <td>
                                        <asp:DropDownList ID="SelectTestDropDownList" AutoPostBack="True" runat="server" Height="27px" Width="223px" OnSelectedIndexChanged="SelectTestDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="nameOfProperty">Fee</th>
                                    <td>
                                        <asp:TextBox ID="FeeTextBox" ReadOnly="True" runat="server" Height="25px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="AddButton" runat="server" Text="Add" Width="89px" OnClick="AddButton_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="ConfirmationLabel" runat="server" BackColor="skyblue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>
                    <div>
                        <asp:GridView ID="ShowTestsGridView" runat="server" AutoGenerateColumns="False" Width="450px" style="margin-left: 5px">
                            <Columns>
                                <asp:TemplateField HeaderText="SL">
                                    <ItemTemplate>
                                        <span>
                                            <%#Container.DataItemIndex + 1%>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Test Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#Eval("TestName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fee">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#Eval("Fee") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <table>
                            <tr>
                                <td class="auto-style2"></td>
                                <th class="auto-style1">
                                    <asp:Label ID="TotalLabel" runat="server" Text="Total"></asp:Label>
                                </th>
                                <td>
                                    <asp:TextBox ID="TotalTextBox" ReadOnly="True" runat="server" Width="140px" Height="25px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="SaveButton" runat="server" Text="Save" OnClick="SaveButton_Click" Width="89px" />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="MessageLabel" runat="server" BackColor="skyblue"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
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
            $('#DateOfBirthTextBox').datepicker({
                format: "dd-mm-yyyy",
                autoclose: true,
                todayHighlight: true,
                endDate: "today"
            });
        })
    </script>
</body>
</html>
