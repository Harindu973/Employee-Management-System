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
using System.IO;

namespace Employee_Management_System
{
    public partial class Reg : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='E:\C# Databases\EMP.mdf';Integrated Security=True;Connect Timeout=30");
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
            string Nic = txtNic.Text;
            string Phone = txtPhone.Text;
            string Dob = txtDob.Text;
            string Add = txtAdd.Text;
            string Gender;
            if (RMale.Checked)
            {
                Gender = "Male";
            }
            else 
            {
                Gender = "Female";
            }
            

            byte[] images = null;
            FileStream Streem = new FileStream(imgLocation,FileMode.Open,FileAccess.Read);
            BinaryReader brs = new BinaryReader(Streem);
            images=brs.ReadBytes((int)Streem.Length);


            // SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='E:\C# Databases\EMP.mdf';Integrated Security=True;Connect Timeout=30");
            conn.Open();
            string query = "INSERT INTO EMPDetails values('" + Fname + "','" + Lname + "','" + Desig + "','" + Nic + "','" + Phone + "','" + Dob + "','" + Add + "','" + Gender + "',@images )";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@images", images));

           



            try
            {
               
                cmd.ExecuteNonQuery();
                conn.Close();  
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

        string imgLocation = "";
       // SqlCommand cmd;

        private void xuiButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imgLocation = dialog.FileName.ToString();
                pictureBox1.ImageLocation = imgLocation;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Employee em = new Employee();
            this.Hide();
            em.Show();
        }
    }
}
