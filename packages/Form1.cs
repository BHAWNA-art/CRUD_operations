using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace CrudOperations
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=TXCHD-PC-031;Initial Catalog=TestBase;Integrated Security=True");
        public int StudentID;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM StudentTb WHERE StudentID=@Id", con);
                cmd.CommandType = CommandType.Text;
                
                cmd.Parameters.AddWithValue("@Id", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student is deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentRecord();
                ResetFormControls();
            }
            else
            {
                MessageBox.Show("Please select a student to delete the record", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentRecord();
        }

        private void GetStudentRecord()
        {
            SqlCommand cmd = new SqlCommand("Select * from StudentTb", con);
            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            StudentRecordDataGridView.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(IsValid())
            {
                SqlCommand cmd = new SqlCommand("Insert into StudentTb values(@name,@fname,@roll,@mobile,@address)",con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@fname", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@roll", txtRollNo.Text);
                cmd.Parameters.AddWithValue("@mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New student is successfully saved in the database", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentRecord();
                ResetFormControls();
            }
        }

        private bool IsValid()
        {
            if(txtStudentName.Text == string.Empty)
            {
                MessageBox.Show("Student name is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResetFormControls();
        }

        private void ResetFormControls()
        {
            StudentID = 0;
            txtStudentName.Clear();
            txtFatherName.Clear();
            txtMobile.Clear();
            txtRollNo.Clear();
            txtAddress.Clear();

            txtStudentName.Focus();
        }

        private void StudentRecordDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentID = Convert.ToInt32(StudentRecordDataGridView.SelectedRows[0].Cells[0].Value);
            txtStudentName.Text = StudentRecordDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtFatherName.Text = StudentRecordDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtRollNo.Text = StudentRecordDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtAddress.Text = StudentRecordDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            txtMobile.Text = StudentRecordDataGridView.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(StudentID>0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE StudentTb SET Name=@name,FatherName=@fname,RollNo=@roll,Mobile=@mobile,Address=@address WHERE StudentID=@Id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@fname", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@roll", txtRollNo.Text);
                cmd.Parameters.AddWithValue("@mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Id", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student Information is updated successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentRecord();
                ResetFormControls();
            }
            else
            {
                MessageBox.Show("Please select a student to update his information", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          }
    }
}
