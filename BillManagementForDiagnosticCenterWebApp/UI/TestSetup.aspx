<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestSetup.aspx.cs" Inherits="BillManagementForDiagnosticCenterWebApp.UI.TestSetup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test Setup</title>
    <link href="../Content/Styles.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 252px;
        }

        .auto-style3 {
            height: 30px;
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
                    <fieldset>
                        <legend>Test Setup</legend>
                        <div>
                            <fieldset>
                                <table style="height: 112px; width: 449px">
                                    <tr>
                                        <th>Test Name</th>
                                        <td class="auto-style1">
                                            <asp:TextBox ID="TestNameTextBox" runat="server" Height="30px" Width="213px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>Fee</th>
                                        <td class="auto-style1">
                                            <asp:TextBox ID="FeeTextBox" TextMode="Number" runat="server" Width="213px" Height="30px"></asp:TextBox>
                                        </td>
                                        <td>BDT</td>
                                    </tr>
                                    <tr>
                                        <th class="auto-style3">Test Type</th>
                                        <td class="auto-style3">
                                            <asp:DropDownList ID="TestTypeDropDownList" runat="server" Width="213px" Height="35px"></asp:DropDownList>
                                            <td class="auto-style3"></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td class="auto-style1">
                                            <asp:Button ID="TestNameButton" runat="server" Text="Save" Height="33px" Width="102px" OnClick="TestNameButton_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td class="auto-style1">
                                            <asp:Label ID="ConfirmationLabel" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                        <div>
                            <asp:GridView ID="ShowAllTestsGridView" runat="server" AutoGenerateColumns="False" Width="528px" style="margin-left: 5px">
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
                                    <asp:TemplateField HeaderText="Test Type">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#Eval("TestTypeName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </fieldset>
                </div>
            </form>
        </div>

        <footer>Copyright &copy; ArmanSheikh</footer>

    </div>
</body>
</html>
