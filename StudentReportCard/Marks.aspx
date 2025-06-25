<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Marks.aspx.cs" Inherits="StudentReportCard.Marks" %>


<!DOCTYPE html>
<html>
<head runat="server">
    <title>Enter Marks</title>
    <style>
        body { font-family: Arial; background-color: #f8f9fa; padding: 30px; }
        .box { background: white; padding: 20px; width: 400px; border-radius: 10px; box-shadow: 0 0 10px #ccc; margin-bottom: 30px; }
        label { display: block; margin-top: 10px; }
        input, select { width: 100%; padding: 6px; margin-top: 5px; }
        .btn { margin-top: 15px; padding: 10px 20px; background-color: #28a745; color: white; border: none; cursor: pointer; }
        .btn:hover { background-color: #218838; }
        table { width: 100%; border-collapse: collapse; margin-top: 30px; }
        th, td { border: 1px solid #ccc; padding: 8px; text-align: center; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="box">
            <h2>Enter Marks</h2>
            <!-- Hidden field to store MarkId when editing -->
            <asp:HiddenField ID="hfMarkId" runat="server" />
            
            <label>Student</label>
            <asp:DropDownList ID="ddlStudents" runat="server" />
            
            <label>Subject</label>
            <asp:TextBox ID="txtSubject" runat="server" />
            
            <label>Term</label>
            <asp:DropDownList ID="ddlTerm" runat="server">
                <asp:ListItem Text="Half" Value="Half" />
                <asp:ListItem Text="Final" Value="Final" />
            </asp:DropDownList>
            
            <label>Periodic</label>
            <asp:TextBox ID="txtPeriodic" runat="server" TextMode="Number" />
            
            <label>Notebook</label>
            <asp:TextBox ID="txtNotebook" runat="server" TextMode="Number" />
            
            <label>Activity</label>
            <asp:TextBox ID="txtActivity" runat="server" TextMode="Number" />
            
            <label>Exam</label>
            <asp:TextBox ID="txtExam" runat="server" TextMode="Number" />
            
            <asp:Button ID="btnSave" runat="server" Text="Save Marks" CssClass="btn" OnClick="btnSave_Click" />
        </div>

        <asp:GridView ID="gvMarks" runat="server" AutoGenerateColumns="false" OnRowCommand="gvMarks_RowCommand">
            <Columns>
                <asp:BoundField DataField="MarkId" HeaderText="ID" />
                <asp:BoundField DataField="Name" HeaderText="Student" />
                <asp:BoundField DataField="SubjectName" HeaderText="Subject" />
                <asp:BoundField DataField="Term" HeaderText="Term" />
                <asp:BoundField DataField="Total" HeaderText="Total" />
                <asp:BoundField DataField="Grade" HeaderText="Grade" />
                <asp:ButtonField Text="Edit" ButtonType="Button" CommandName="EditMark" />
                <asp:ButtonField Text="Delete" ButtonType="Button" CommandName="DeleteMark" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>