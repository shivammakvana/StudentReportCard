<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StudentReportCard.Default" %>



<!DOCTYPE html>
<html>
<head runat="server">
    <title>Student Report Card - Home</title>
    <style>
        body { font-family: Arial; background-color: #f0f0f0; }
        .container { width: 50%; margin: 100px auto; text-align: center; }
        a.button {
            display: block;
            margin: 15px;
            padding: 15px;
            background-color: #4CAF50;
            color: white;
            text-decoration: none;
            border-radius: 5px;
            font-size: 18px;
        }
        a.button:hover { background-color: #45a049; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Student Report Card System</h1>
            <a href="Students.aspx" class="button">Manage Students</a>
            <a href="Marks.aspx" class="button">Enter Marks</a>
            <a href="ReportCard.aspx" class="button">View Report Card</a>
        </div>
    </form>
</body>
</html>

