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
            string Desig = comboDesig.Text;
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
            //var time = DateTime.Now;
            string query = "INSERT INTO EMPDetails values('" + Fname + "','" + Lname + "','" + Desig + "','" + Nic + "','" + Phone + "','" + Dob + "','" + Add + "','" + Gender + "',@images )";
            //string attquery = "INSERT INTO Attendance values('" + Fname + "','" + Fname + "','1','','')";
            SqlCommand cmd = new SqlCommand(query, conn);
            //SqlCommand attcmd = new SqlCommand(attquery, conn);
            cmd.Parameters.Add(new SqlParameter("@images", images));

           



            try
            {
               
                cmd.ExecuteNonQuery();
                //attcmd.ExecuteNonQuery();
                
                
                MessageBox.Show("You are now Registerd...!!!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something's Going wrong...!  Plz Contact a Developer..." + ex);
            }
            finally
            {
                conn.Close();
            }



            //Attendance Sheet reg


            
           
             
            
            
            try
            {
                conn.Open();
                string selqry = "SELECT * FROM EMPDetails where NIC = '" + Nic + "' ";
                SqlCommand selcmd = new SqlCommand(selqry, conn);
                SqlDataReader reader = selcmd.ExecuteReader();
                reader.Read();

                string EMPID = reader["EMP_ID"].ToString();
                reader.Close();

                string attquery = "INSERT INTO Attendance values('" + EMPID + "','" + Fname + "','Absent','','','0','0','0','0')";

                SqlCommand attcmd = new SqlCommand(attquery, conn);

                attcmd.ExecuteNonQuery();
                conn.Close();
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something's Going wrong in Attendance sheet reg...!  Plz Contact a Developer..." + ex);
            }
            catch(Exception x)
            {
                MessageBox.Show("" + x);
            }
            finally
            {
                conn.Close();
            }





            //Attendance Monthy table reg

            try
            {
                conn.Open();
                string selqry = "SELECT * FROM EMPDetails where NIC = '" + Nic + "' ";
                SqlCommand selcmd = new SqlCommand(selqry, conn);
                SqlDataReader reader = selcmd.ExecuteReader();
                reader.Read();

                string EMPID = reader["EMP_ID"].ToString();
                reader.Close();

                //string attquery = "INSERT INTO Attendance values('" + EMPID + "','" + Fname + "','Absent','','','0','0','0')";
                string attmonthquery = "INSERT INTO MonthlyAtt values('" + EMPID + "','" + Fname + "','" + Lname + "','" + Nic + "','0','0','0','0')";

                //SqlCommand attcmd = new SqlCommand(attquery, conn);
                SqlCommand attmonthcmd = new SqlCommand(attmonthquery, conn);

                //attcmd.ExecuteNonQuery();
                attmonthcmd.ExecuteNonQuery();
                conn.Close();
                //MessageBox.Show("You are now Registerd...!!!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something's Going wrong in Monthly Attendance sheet reg...!  Plz Contact a Developer..." + ex);
            }
            catch (Exception x)
            {
                MessageBox.Show("" + x);
            }
            finally
            {
                conn.Close();
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
            dialog.Filter = "All files(*.*)|*.*|jpg files(*.jpg)|*.jpg|png files(*.png)|*.png";
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

        private void comboDesig_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboDesig.DisplayMember = "Text";
            comboDesig.ValueMember = "Value";

            var items = new[] {
            new { Text = "report A", Value = "reportA" },
            new { Text = "report B", Value = "reportB" },
            new { Text = "report C", Value = "reportC" },
            new { Text = "report D", Value = "reportD" },
            new { Text = "report E", Value = "reportE" }
};

            comboDesig.DataSource = items;
        }
    }
}
