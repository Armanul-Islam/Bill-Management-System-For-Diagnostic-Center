<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestTypeSetup.aspx.cs" Inherits="BillManagementForDiagnosticCenterWebApp.UI.TestTypeSetup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test Type Setup</title>
    <link href="../Content/Styles.css" rel="stylesheet" />
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
                        <legend>Test Type Setup</legend>
                        <div>
                            <fieldset>
                                <table style="height: 112px; width: 449px">
                                    <tr>
                                        <th>Type Name</th>
                                        <td>
                                            <asp:TextBox ID="TypeNameTextBox" runat="server" Height="30px" Width="214px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="TypeNameButton" runat="server" Text="Save" Height="29px" OnClick="TypeNameButton_Click" Width="102px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Label ID="ConfirmationLabel" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                        <div>
                            <asp:GridView ID="ShowAllTestTypesGridView" runat="server" AutoGenerateColumns="False" Width="415px" style="margin-left: 5px">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <span>
                                                <%#Container.DataItemIndex + 1%>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#Eval("TypeName") %>'></asp:Label>
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
