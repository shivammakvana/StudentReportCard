<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Students.aspx.cs" Inherits="StudentReportCard.Students" %>



<!DOCTYPE html>
<html>
<head runat="server">
    <title>Manage Students</title>
    <style>
        body { font-family: Arial; margin: 30px; background-color: #f8f9fa; }
        label { display: block; margin-top: 10px; }
        input, select { width: 300px; padding: 5px; margin-top: 5px; }
        .form-box { background: #fff; padding: 20px; border-radius: 10px; width: 400px; float: left; margin-right: 40px; box-shadow: 0 0 10px #ccc; }
        .grid-box { float: left; }
        .btn { padding: 8px 16px; background: #007BFF; color: white; border: none; margin-top: 15px; cursor: pointer; }
        .btn:hover { background: #0056b3; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-box">
            <h2>Add / Edit Student</h2>
            <asp:HiddenField ID="hfStudentId" runat="server" />
            <label>Roll No</label>
            <asp:TextBox ID="txtRollNo" runat="server" />
            <label>Admission No</label>
            <asp:TextBox ID="txtAdmNo" runat="server" />
            <label>Name</label>
            <asp:TextBox ID="txtName" runat="server" />
            <label>DOB</label>
            <asp:TextBox ID="txtDOB" runat="server" TextMode="Date" />
            <label>Class & Section</label>
            <asp:TextBox ID="txtClassSection" runat="server" />
            <label>Mother's Name</label>
            <asp:TextBox ID="txtMotherName" runat="server" />
            <label>Father's Name</label>
            <asp:TextBox ID="txtFatherName" runat="server" />
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn" OnClick="btnSave_Click" />
        </div>

        <div class="grid-box">
            <h2>Student List</h2>
            <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns="False" OnRowCommand="gvStudents_RowCommand">
                <Columns>
                    <asp:BoundField DataField="StudentId" HeaderText="ID" />
                    <asp:BoundField DataField="RollNo" HeaderText="Roll No" />
                    <asp:BoundField DataField="AdmNo" HeaderText="Adm No" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="DOB" HeaderText="DOB" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="ClassSection" HeaderText="Class" />
                    <asp:BoundField DataField="MotherName" HeaderText="Mother" />
                    <asp:BoundField DataField="FatherName" HeaderText="Father" />
                    <asp:ButtonField Text="Edit" ButtonType="Button" CommandName="EditStudent" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>

