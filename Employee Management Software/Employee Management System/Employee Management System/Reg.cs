using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace Employee_Management_System
{
    public partial class Reg : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();


        public Reg()
        {
            InitializeComponent();
        }

        private void Reg_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Fname = txtFname.Text;
            string Lname = txtLname.Text;
            string Desig = txtDesig.Text;
            string Phone = txtPhone.Text;
            string Dob = txtDob.Text;
            string Add = txtAdd.Text;
            string Gender = txtGender.Text;
            string Photo = picPhoto.Text;

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='E:\C# Databases\EMP.mdf';Integrated Security=True;Connect Timeout=30");
            string query = "INSERT INTO EMPDetails values('" + Fname + "','" + Lname + "','" + Desig + "','" + Phone + "','" + Dob + "','" + Add + "','" + Gender + "','')";
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("You are now Registerd...!!!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Somethig's Going wrong...!  Plz Contact a Developer..." + ex);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PnlMove_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PnlMove_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void saveFileDialog2_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
