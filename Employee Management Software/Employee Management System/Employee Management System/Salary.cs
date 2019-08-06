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
    public partial class Salary : Form
    {

        SqlConnection constring = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='E:\C# Databases\EMP.mdf';Integrated Security=True;Connect Timeout=30");


        public Salary()
        {
            InitializeComponent();



        }

        private void Back_Click(object sender, EventArgs e)
        {
            Employee em = new Employee();
            em.Show();
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string key = txtSearch.Text;


            string qry = "SELECT * FROM MonthlyAtt EMPID = '" + key + "' ";
            //string qFname = "SELECT * FROM EMPDetails where EMP_ID = '" + key + "' ";

            //EMP Details Table
            SqlCommand cmd = new SqlCommand(qry, constring);

            try
            {
                constring.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                label12.Text = reader["FirstName"].ToString();
                label11.Text = reader["Designation"].ToString();



                reader.Close();
            }
            catch (SqlException se)
            {
                MessageBox.Show("" + se);
            }




            //Ateendance table
            SqlDataAdapter da = new SqlDataAdapter(qry, constring);
            DataSet ds = new DataSet();


            da.Fill(ds, "MonthlyAtt");
            DGV1.DataSource = ds.Tables["MonthlyAtt"];


            //image retrive
            cmd = new SqlCommand("SELECT * FROM EMPDetails where EMP_ID = '" + key + "' ", constring);
            SqlDataAdapter daa = new SqlDataAdapter(cmd);
            DataSet dss = new DataSet();
            daa.Fill(dss);
            if (dss.Tables[0].Rows.Count > 0)
            {
                MemoryStream ms = new MemoryStream((byte[])dss.Tables[0].Rows[0]["Photo"]);
                pictureBox1.Image = new Bitmap(ms);
            }

            constring.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
