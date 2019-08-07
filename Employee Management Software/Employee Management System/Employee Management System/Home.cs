using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Employee_Management_System
{
    public partial class Home: Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

             Scan sc = new Scan();
             this.Hide();
             sc.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LogIn lg = new LogIn();
            this.Hide();
            lg.Show();

        }
    }
}
