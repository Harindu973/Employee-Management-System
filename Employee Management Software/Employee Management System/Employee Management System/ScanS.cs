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
    public partial class ScanS : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();



        FilterInfoCollection capturedev;
        private VideoCaptureDevice finalframe;

        public ScanS()
        {
            InitializeComponent();
        }

        private void ScanS_Load(object sender, EventArgs e)
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
                MessageBox.Show("" + ex);
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

                             MessageBox.Show("" + QRval);



                            Employee em = new Employee();
                            this.Hide();
                            em.Show();
                        

                              em.txtSearch.Text=QRval.ToString();

                        }
                    }
                    catch (Exception ex)
                    {
                        // MessageBox.Show("" + ex);
                    }


                }

            }
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

        private void ScanS_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (finalframe != null)
                if (finalframe.IsRunning == true)
                {
                    finalframe.Stop();
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
