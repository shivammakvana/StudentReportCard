using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StudentReportCard
{
    public partial class ReportCard : System.Web.UI.Page
    {
        string conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\shiva\source\repos\StudentReportCard\StudentReportCard\App_Data\Database1.mdf;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadClasses();
            }
        }

        void LoadClasses()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT ClassSection FROM Students", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                ddlClass.Items.Clear();
                ddlClass.Items.Add("-- Select Class --");
                while (dr.Read())
                {
                    ddlClass.Items.Add(dr["ClassSection"].ToString());
                }
            }
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlStudents.Items.Clear();
            lblStudentDetails.Text = "";
            gvHalf.DataSource = null;
            gvHalf.DataBind();
            gvFinal.DataSource = null;
            gvFinal.DataBind();
            lblTotals.Text = "";

            if (ddlClass.SelectedIndex > 0)
                LoadStudentsByClass(ddlClass.SelectedItem.Text);
        }

        void LoadStudentsByClass(string classSection)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT StudentId, Name FROM Students WHERE ClassSection=@Class", con);
                cmd.Parameters.AddWithValue("@Class", classSection);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                ddlStudents.Items.Clear();
                ddlStudents.Items.Add("-- Select Student --");
                while (dr.Read())
                {
                    ddlStudents.Items.Add(new ListItem(dr["Name"].ToString(), dr["StudentId"].ToString()));
                }
            }
        }

        protected void ddlStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStudents.SelectedIndex == 0)
                return;

            LoadStudentDetails();
            LoadMarks("Half", gvHalf);
            LoadMarks("Final", gvFinal);
            CalculateTotal();
        }

        void LoadStudentDetails()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Students WHERE StudentId=@StudentId", con);
                cmd.Parameters.AddWithValue("@StudentId", ddlStudents.SelectedValue);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lblStudentDetails.Text = $"<b>Roll No:</b> {dr["RollNo"]} &nbsp;&nbsp; <b>Adm No:</b> {dr["AdmNo"]} &nbsp;&nbsp; <b>Class/Section:</b> {dr["ClassSection"]}<br/>" +
                                             $"<b>Student Name:</b> {dr["Name"]} &nbsp;&nbsp; <b>Date of Birth:</b> {Convert.ToDateTime(dr["DOB"]).ToString("dd/MM/yyyy")}<br/>" +
                                             $"<b>Mother's Name:</b> {dr["MotherName"]} &nbsp;&nbsp; <b>Father's Name:</b> {dr["FatherName"]}";
                }
            }
        }

        void LoadMarks(string term, GridView grid)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string query = "SELECT SubjectName, Periodic, Notebook, Activity, Exam, Total, Grade FROM Marks WHERE StudentId = @StudentId AND Term = @Term";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@StudentId", ddlStudents.SelectedValue);
                cmd.Parameters.AddWithValue("@Term", term);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grid.DataSource = dt;
                grid.DataBind();
            }
        }

        void CalculateTotal()
        {
            int grandTotal = 0;
            int maxTotal = 0;

            foreach (GridViewRow row in gvHalf.Rows)
            {
                grandTotal += Convert.ToInt32(row.Cells[5].Text);
                maxTotal += 100;
            }

            foreach (GridViewRow row in gvFinal.Rows)
            {
                grandTotal += Convert.ToInt32(row.Cells[5].Text);
                maxTotal += 100;
            }

            double percentage = (double)grandTotal / maxTotal * 100;
            string grade = GetOverallGrade(percentage);
            lblTotals.Text = $"<br/><b>Grand Total:</b> {grandTotal}/{maxTotal} &nbsp;&nbsp; <b>Percentage:</b> {percentage:F2}% &nbsp;&nbsp; <b>Overall Grade:</b> {grade}";
        }

        string GetOverallGrade(double percent)
        {
            if (percent >= 90) return "A+";
            else if (percent >= 80) return "A";
            else if (percent >= 70) return "B";
            else if (percent >= 60) return "B";
            else if (percent >= 50) return "C";
            else if (percent >= 40) return "D";
            else return "F";
        }
    }
}
