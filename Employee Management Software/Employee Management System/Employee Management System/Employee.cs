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
using System.IO;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.QrCode;

namespace Employee_Management_System
{
    public partial class Employee : Form
    {
        SqlConnection constring = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='E:\C# Databases\EMP.mdf';Integrated Security=True;Connect Timeout=30");

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();




        public Employee()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void Employee_Load(object sender, EventArgs e)
        {
            constring.Open();
            string qry = "SELECT * From Attendance";
            SqlDataAdapter da = new SqlDataAdapter(qry, constring);
            DataSet ds = new DataSet();

            da.Fill(ds, "Attendance");
            DGV1.DataSource = ds.Tables["Attendance"];

            constring.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void btnSearch_Click(object sender, EventArgs e)
        {

            string key = txtSearch.Text;


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

        private void Add_Click(object sender, EventArgs e)
        {
            Reg rg = new Reg();
            this.Hide();
            rg.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ScanS sc = new ScanS();
            this.Close();
            sc.Show();
          
            

            
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminHome ad = new AdminHome();
            ad.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Application.Exit();
               
        }

        private void PnlMove_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Salary sa = new Salary();
            
            this.Close();
            sa.Show();
        }
    }
}
