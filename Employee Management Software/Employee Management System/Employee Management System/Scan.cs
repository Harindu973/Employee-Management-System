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

namespace Employee_Management_System
{
    public partial class Scan : Form
    {
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
            comboBox1.SelectedIndex = -1;
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
                { // here decode the qr and do matching with our string “abobaker” every tick 
                    Result res = red.Decode((Bitmap)pictureBox1.Image);
                    try
                    {
                        string dec = res.ToString().Trim();
                        if (dec == "Harindu")
                        {
                            timer1.Stop(); MessageBox.Show(" Matching...!!! ");
                        }
                    }
                    catch (Exception ex)
                    {
                          
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
    }


    
}
