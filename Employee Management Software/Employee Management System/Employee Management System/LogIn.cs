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

namespace Employee_Management_System
{
    public partial class LogIn : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public LogIn()
        {
            InitializeComponent();
        }

        private void PnlMove_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void LogIn_Load(object sender, EventArgs e)
        {

        }

        private void PnlMove_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string uname = txtUname.Text;
            string pw = txtPw.Text;
            bool Match;

            if (uname == string.Empty || pw == string.Empty)
            {
                MessageBox.Show("Please check...  Something's Missing...!!!");
                txtUname.Text = "";
                txtPw.Text = "";
            }
            else
            {
                Encapsulation en = new Encapsulation();
                en.setValues(uname, pw);
                Match = en.getValues();

                if (Match == false)
                {
                    MessageBox.Show("Wrong Username Or Password...!!!");
                    txtUname.Text = "";
                    txtPw.Text = "";
                }
                else
                {
                    Home home = new Home();
                    this.Hide();
                    home.Show();

                }
            }

        }
    }
}
