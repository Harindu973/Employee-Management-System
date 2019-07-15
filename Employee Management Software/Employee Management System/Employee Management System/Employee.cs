using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Employee_Management_System
{
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void Employee_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string key = txtSearch.Text;

            SqlConnection constring = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='E:\C# Databases\EMP.mdf';Integrated Security=True;Connect Timeout=30");
            //string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='E:\C# Databases\EMP.mdf';Integrated Security=True;Connect Timeout=30";
            string qry = "SELECT * FROM Attendance where EMPID = '"+key+"' ";
            string qFname = "SELECT * FROM EMPDetails where EMP_ID = '" + key + "' ";

            //EMP Details Table
            SqlCommand cmd = new SqlCommand(qFname, constring);

            try
            {
                constring.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                txtFname.Text = reader["FirstName"].ToString();
                txtLname.Text = reader["LastName"].ToString();
                txtDesig.Text = reader["Designation"].ToString();
                txtNic.Text = reader["NIC"].ToString();
                txtPhone.Text = reader["PhoneNO"].ToString();
                txtAdd.Text = reader["Address"].ToString();
                txtGender.Text = reader["Gender"].ToString();
                txtDob.Text = reader["DOB"].ToString();


                reader.Close();
            }
            catch(SqlException se)
            {
                MessageBox.Show("" + se);
            }




            //Ateendance table
            SqlDataAdapter da = new SqlDataAdapter(qry, constring);
            DataSet ds = new DataSet();

            da.Fill(ds, "Attendance");
            DGV1.DataSource = ds.Tables["Attendance"];




        }
    }
}
