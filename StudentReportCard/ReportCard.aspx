<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportCard.aspx.cs" Inherits="StudentReportCard.ReportCard" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Report Card</title>
    <style>
        body { font-family: Arial; background-color: #fff; padding: 20px; }
        .container { width: 100%; max-width: 900px; margin: auto; border: 2px solid #000; padding: 20px; }
        .header { text-align: center; }
        .header h1 { margin: 0; color: #004080; }
        .section-title { background-color: #004080; color: white; padding: 5px; text-align: center; margin-top: 20px; }
        table { width: 100%; border-collapse: collapse; margin-top: 10px; }
        table, th, td { border: 1px solid #000; }
        th, td { padding: 5px; text-align: center; }
        .student-info, .signature { margin-top: 20px; }
        .signature td { border: none; text-align: center; padding-top: 50px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <img src=""C:\Users\shiva\source\repos\StudentReportCard\StudentReportCard\logo.png"" style="float:left; height:80px;">
                <img src=""C:\Users\shiva\source\repos\StudentReportCard\StudentReportCard\logo.png"" style="float:right; height:80px;">
                <h1>The Shepherd High Secondary School</h1>
                <p>Annual Report Card</p>
            </div>

            <div class="student-info">
                <asp:DropDownList ID="ddlClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" />
                <asp:DropDownList ID="ddlStudents" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStudents_SelectedIndexChanged" />
                <asp:Label ID="lblStudentDetails" runat="server" Text="" Font-Bold="true"></asp:Label>
            </div>

            <div class="section-title">Half Yearly Evaluation</div>
            <asp:GridView ID="gvHalf" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="SubjectName" HeaderText="Subject" />
                    <asp:BoundField DataField="Periodic" HeaderText="Periodic (10)" />
                    <asp:BoundField DataField="Notebook" HeaderText="Notebook (5)" />
                    <asp:BoundField DataField="Activity" HeaderText="Activity (5)" />
                    <asp:BoundField DataField="Exam" HeaderText="Half Yearly (80)" />
                    <asp:BoundField DataField="Total" HeaderText="Total (100)" />
                    <asp:BoundField DataField="Grade" HeaderText="Grade" />
                </Columns>
            </asp:GridView>

            <div class="section-title">Final Evaluation</div>
            <asp:GridView ID="gvFinal" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="SubjectName" HeaderText="Subject" />
                    <asp:BoundField DataField="Periodic" HeaderText="Periodic (10)" />
                    <asp:BoundField DataField="Notebook" HeaderText="Notebook (5)" />
                    <asp:BoundField DataField="Activity" HeaderText="Activity (5)" />
                    <asp:BoundField DataField="Exam" HeaderText="Final (80)" />
                    <asp:BoundField DataField="Total" HeaderText="Total (100)" />
                    <asp:BoundField DataField="Grade" HeaderText="Grade" />
                </Columns>
            </asp:GridView>

            <asp:Label ID="lblTotals" runat="server" Font-Bold="true"></asp:Label>

            <div class="section-title">Traits Evaluation</div>
            <table>
                <tr><th>Traits</th><th>Half Yearly</th><th>Final</th></tr>
                <tr><td>Art Education</td><td>E</td><td>B</td></tr>
                <tr><td>Discipline</td><td>E</td><td>B</td></tr>
                <tr><td>Health and Physical Education</td><td>B</td><td>B</td></tr>
                <tr><td>GK VE and Computer</td><td>B</td><td>A</td></tr>
            </table>

            <p><b>Result:</b> Passed and Promoted to Class VI</p>

            <table class="signature">
                <tr>
                    <td>Place: Ujjain</td>
                    <td>Signature of Class Teacher</td>
                    <td>Signature of Principal</td>
                </tr>
                <tr>
                    <td>Date: <%= DateTime.Now.ToString("dd/MM/yyyy") %></td>
                    <td>______________________</td>
                    <td>______________________</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
