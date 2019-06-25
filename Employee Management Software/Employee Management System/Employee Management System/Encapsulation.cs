using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_System
{
    class Encapsulation
    {
        private string Vname = "Harindu";
        private string Vpw = "H123";
        private string Pname;
        private string Ppw;
        private bool verify = false;

        public void setValues(string uname, string pw)
        {
            Pname = uname;
            Ppw = pw;


        }

        public bool getValues()
        {
            if (Pname == Vname && Ppw == Vpw)
            {
                verify = true;
                return verify;
            }
            else
            {
                return verify; 
            }
        }

        
         
    }
}
