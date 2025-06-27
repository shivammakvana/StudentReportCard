using StudentReportCard;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace StudentReportCard
{
    public partial class Students : System.Web.UI.Page
    {
        string conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\shiva\source\repos\StudentReportCard\StudentReportCard\App_Data\Database1.mdf;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStudents();
            }
        }

        protected void LoadStudents()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Students", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvStudents.DataSource = dt;
                gvStudents.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                string query = "";
                if (string.IsNullOrEmpty(hfStudentId.Value)) 
                {
                    query = @"INSERT INTO Students (RollNo, AdmNo, Name, DOB, ClassSection, MotherName, FatherName)
                          VALUES (@RollNo, @AdmNo, @Name, @DOB, @ClassSection, @MotherName, @FatherName)";
                }
                else 
                {
                    query = @"UPDATE Students SET RollNo=@RollNo, AdmNo=@AdmNo, Name=@Name, DOB=@DOB,
                          ClassSection=@ClassSection, MotherName=@MotherName, FatherName=@FatherName
                          WHERE StudentId=@StudentId";
                }

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@RollNo", txtRollNo.Text);
                cmd.Parameters.AddWithValue("@AdmNo", txtAdmNo.Text);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(txtDOB.Text));
                cmd.Parameters.AddWithValue("@ClassSection", txtClassSection.Text);
                cmd.Parameters.AddWithValue("@MotherName", txtMotherName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                if (!string.IsNullOrEmpty(hfStudentId.Value))
                    cmd.Parameters.AddWithValue("@StudentId", hfStudentId.Value);

                cmd.ExecuteNonQuery();
            }

            ClearFields();
            LoadStudents();
        }

        protected void gvStudents_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditStudent")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int studentId = Convert.ToInt32(gvStudents.Rows[index].Cells[0].Text);

                using (SqlConnection con = new SqlConnection(conStr))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Students WHERE StudentId=@StudentId", con);
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        hfStudentId.Value = dr["StudentId"].ToString();
                        txtRollNo.Text = dr["RollNo"].ToString();
                        txtAdmNo.Text = dr["AdmNo"].ToString();
                        txtName.Text = dr["Name"].ToString();
                        txtDOB.Text = Convert.ToDateTime(dr["DOB"]).ToString("yyyy-MM-dd");
                        txtClassSection.Text = dr["ClassSection"].ToString();
                        txtMotherName.Text = dr["MotherName"].ToString();
                        txtFatherName.Text = dr["FatherName"].ToString();
                    }
                }
            }
        }

        protected void ClearFields()
        {
            hfStudentId.Value = "";
            txtRollNo.Text = txtAdmNo.Text = txtName.Text = txtDOB.Text = txtClassSection.Text = txtMotherName.Text = txtFatherName.Text = "";
        }
    }
}
