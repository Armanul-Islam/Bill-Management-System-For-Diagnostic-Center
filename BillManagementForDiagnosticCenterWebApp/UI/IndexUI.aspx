<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndexUI.aspx.cs" Inherits="BillManagementForDiagnosticCenterWebApp.UI.IndexUI" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Bill Management System</title>
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
            <img id="displayImage" src="Heart-Diagnostic-Centre.jpg" alt="Diagnostic_Center">
        </div>

        <footer>Copyright &copy; ArmanSheikh</footer>
    </div>
</body>
</html>
