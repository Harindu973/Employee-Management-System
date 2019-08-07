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
                             timer1.Stop();

                            string QRval = dec;
                             
                            // MessageBox.Show("" + QRval);

                            SqlConnection constring = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='E:\C# Databases\EMP.mdf';Integrated Security=True;Connect Timeout=30");

                            string qry = "SELECT * FROM EMPDetails where EMP_ID = '"+QRval+"' ";
                            SqlCommand cmd = new SqlCommand(qry, constring);

                            

                            constring.Open();
                            SqlDataReader reader = cmd.ExecuteReader();

                            //reader.Read();

                            if (reader.Read())
                            {
                                var time = DateTime.Now;
                                string name = reader["FirstName"].ToString();
                                reader.Close();
                                constring.Close();

                                try
                                {
                                    ThirdParty tp = new ThirdParty();
                                    bool result = tp.CheckAttend(QRval);

                                    constring.Open();
                                    string attqry = "UPDATE Attendance SET Mark = '1' WHERE EMPID='" + QRval + "'";
                                    SqlCommand cmdattIN = new SqlCommand(attqry, constring);
                                    cmdattIN.ExecuteNonQuery();

                                    string attqry2 = "UPDATE Attendance SET Attended = 'Present' WHERE EMPID='" + QRval + "'";
                                    SqlCommand cmdattIN2 = new SqlCommand(attqry2, constring);
                                    cmdattIN2.ExecuteNonQuery();
                                    constring.Close();

                                    if(result == true)
                                    {
                                        MessageBox.Show("Good Evening " + name + "!!!\nYour Leaving time Marked Successfully...!!! \nPress Ok to Finish...");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Good Morning " + name + "  m !!!\nYour Arriving time Marked Successfully...!!! \nPress Ok to Finish...");

                                    }

                                    timer1.Start();
                                }
                                catch(SqlException x)
                                {
                                    MessageBox.Show("" + x);
                                }
                                catch(Exception ex)
                                {
                                    MessageBox.Show("" + ex);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Sorry...!!! Invalid Barcode...Try Again...!!!");
                                timer1.Start();
                            }

                           /* Home hm = new Home();
                             this.Hide();

                             hm.Show();*/

                        }
                    }
                    catch (Exception ex)
                    {
                       // MessageBox.Show("" + ex);
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }


    
}
