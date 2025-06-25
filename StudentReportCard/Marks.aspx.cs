using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace StudentReportCard
{
    public partial class Marks : System.Web.UI.Page
    {
        string conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\shiva\source\repos\StudentReportCard\StudentReportCard\App_Data\Database1.mdf;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStudents();
                LoadMarks();
            }
        }

        // Load students for the drop-down list
        void LoadStudents()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT StudentId, Name FROM Students", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                ddlStudents.Items.Clear();
                while (dr.Read())
                {
                    ddlStudents.Items.Add(new System.Web.UI.WebControls.ListItem(dr["Name"].ToString(), dr["StudentId"].ToString()));
                }
            }
        }

        // Load marks into the GridView
        void LoadMarks()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string query = @"SELECT m.MarkId, s.Name, m.SubjectName, m.Term, m.Total, m.Grade 
                             FROM Marks m JOIN Students s ON m.StudentId = s.StudentId";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvMarks.DataSource = dt;
                gvMarks.DataBind();
            }
        }

        // Save or update marks entry
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Basic validations (can be expanded as needed)
            if (string.IsNullOrEmpty(txtSubject.Text))
                return;

            int periodic = Convert.ToInt32(txtPeriodic.Text);
            int notebook = Convert.ToInt32(txtNotebook.Text);
            int activity = Convert.ToInt32(txtActivity.Text);
            int exam = Convert.ToInt32(txtExam.Text);
            int total = periodic + notebook + activity + exam;
            string grade = GetGrade(total);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                string query = "";

                if (string.IsNullOrEmpty(hfMarkId.Value))  // Insert new marks entry
                {
                    query = @"INSERT INTO Marks (StudentId, SubjectName, Periodic, Notebook, Activity, Exam, Total, Grade, Term)
                          VALUES (@StudentId, @SubjectName, @Periodic, @Notebook, @Activity, @Exam, @Total, @Grade, @Term)";
                }
                else  // Update existing marks entry
                {
                    query = @"UPDATE Marks SET StudentId=@StudentId, SubjectName=@SubjectName, Periodic=@Periodic,
                          Notebook=@Notebook, Activity=@Activity, Exam=@Exam, Total=@Total, Grade=@Grade, Term=@Term
                          WHERE MarkId=@MarkId";
                }

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@StudentId", ddlStudents.SelectedValue);
                cmd.Parameters.AddWithValue("@SubjectName", txtSubject.Text);
                cmd.Parameters.AddWithValue("@Periodic", periodic);
                cmd.Parameters.AddWithValue("@Notebook", notebook);
                cmd.Parameters.AddWithValue("@Activity", activity);
                cmd.Parameters.AddWithValue("@Exam", exam);
                cmd.Parameters.AddWithValue("@Total", total);
                cmd.Parameters.AddWithValue("@Grade", grade);
                cmd.Parameters.AddWithValue("@Term", ddlTerm.SelectedValue);
                if (!string.IsNullOrEmpty(hfMarkId.Value))
                    cmd.Parameters.AddWithValue("@MarkId", hfMarkId.Value);

                cmd.ExecuteNonQuery();
            }

            ClearFields();
            LoadMarks();
        }

        // Handle editing or deleting marks from the GridView
        protected void gvMarks_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditMark")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int markId = Convert.ToInt32(gvMarks.Rows[index].Cells[0].Text);

                using (SqlConnection con = new SqlConnection(conStr))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Marks WHERE MarkId=@MarkId", con);
                    cmd.Parameters.AddWithValue("@MarkId", markId);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        hfMarkId.Value = dr["MarkId"].ToString();
                        ddlStudents.SelectedValue = dr["StudentId"].ToString();
                        txtSubject.Text = dr["SubjectName"].ToString();
                        ddlTerm.SelectedValue = dr["Term"].ToString();
                        txtPeriodic.Text = dr["Periodic"].ToString();
                        txtNotebook.Text = dr["Notebook"].ToString();
                        txtActivity.Text = dr["Activity"].ToString();
                        txtExam.Text = dr["Exam"].ToString();
                    }
                }
            }
            else if (e.CommandName == "DeleteMark")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int markId = Convert.ToInt32(gvMarks.Rows[index].Cells[0].Text);
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Marks WHERE MarkId = @MarkId", con);
                    cmd.Parameters.AddWithValue("@MarkId", markId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadMarks();
            }
        }

        // Determine grade based on total marks
        string GetGrade(int total)
        {
            if (total >= 90) return "A+";
            else if (total >= 80) return "A";
            else if (total >= 70) return "B+";
            else if (total >= 60) return "B";
            else if (total >= 50) return "C";
            else if (total >= 40) return "D";
            else return "F";
        }

        // Clear marks input fields after save/update
        void ClearFields()
        {
            hfMarkId.Value = "";
            txtSubject.Text = txtPeriodic.Text = txtNotebook.Text = txtActivity.Text = txtExam.Text = "";
            ddlTerm.SelectedIndex = 0;
            if (ddlStudents.Items.Count > 0)
                ddlStudents.SelectedIndex = 0;
        }
    }
}