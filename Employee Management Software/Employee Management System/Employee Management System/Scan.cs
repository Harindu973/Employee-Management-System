using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.QrCode;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace Employee_Management_System
{
    public partial class Scan : Form
    {
        

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();



        FilterInfoCollection capturedev;
        private VideoCaptureDevice finalframe;


        public Scan()
        {
            InitializeComponent();
        }

        private void pnlButton_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Scan_Load(object sender, EventArgs e)
        {
            capturedev = new FilterInfoCollection(FilterCategory.VideoInputDevice); foreach (FilterInfo Dev in capturedev)
            {
                comboBox1.Items.Add(Dev.Name);
            }
            comboBox1.SelectedIndex = 0;
        }
        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try { pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone(); }
            catch (Exception ex)
            {
                MessageBox.Show(""+ex);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            {
                BarcodeReader red = new BarcodeReader();
                if (pictureBox1.Image != null)
                { 
                    Result res = red.Decode((Bitmap)pictureBox1.Image);
                    
                    try
                    {
                        string dec = res.ToString().Trim();
                        if (dec != string.Empty)
                        {
                             //timer1.Stop();

                            string QRval = dec;
                             
                            // MessageBox.Show("" + QRval);

                            SqlConnection constring = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='E:\C# Databases\EMP.mdf';Integrated Security=True;Connect Timeout=30");

                            string qry = "SELECT FirstName FROM EMPDetails where EMP_ID = '"+QRval+"' ";
                            SqlCommand cmd = new SqlCommand(qry, constring);



                            constring.Open();
                            SqlDataReader reader = cmd.ExecuteReader();

                            //reader.Read();

                            if (reader.Read())
                            {
                                var time = DateTime.Now;
                                string name = reader["FirstName"].ToString();
                                constring.Close();

                                try
                                {
                                    constring.Open();
                                    string attINqry = "INSERT INTO Attendance values('" + QRval + "','" + name + "','1','" + time + "','') ";
                                    SqlCommand cmdattIN = new SqlCommand(attINqry, constring);
                                    cmdattIN.ExecuteNonQuery();
                                    constring.Close();

                                    MessageBox.Show("Dear " + name + ",Your Attendance Is Marked...!!! Press Ok to Finish...");
                                }
                                catch(SqlException x)
                                {
                                    MessageBox.Show("" + x);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Sorry...!!! You are Not a Employee of us..Try Again...!!!");
                            }

                           /* Home hm = new Home();
                             this.Hide();

                             hm.Show();*/

                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("" + ex);
                    }
 

                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            finalframe = new 
            VideoCaptureDevice(capturedev[comboBox1.SelectedIndex].MonikerString);
            finalframe.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            finalframe.Start();
            timer1.Enabled = true;
            timer1.Start();
        }

        private void Scan_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (finalframe != null)
                if (finalframe.IsRunning == true)
                {
                    finalframe.Stop();
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
    
            Application.Exit();
            
            

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void xuiButton1_Click(object sender, EventArgs e)
        {
            Home hm = new Home();
            this.Hide();
            hm.Show();
        }


    }


    
}
