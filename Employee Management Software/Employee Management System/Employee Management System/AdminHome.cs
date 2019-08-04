using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Employee_Management_System
{
    public partial class AdminHome : Form
    {
        public AdminHome()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection constring = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='E:\C# Databases\EMP.mdf';Integrated Security=True;Connect Timeout=30");

            //Attendance table variables
            int attno;
            string EmpID;
            string Attended;
            int attShortL;
            double attOT = 0;
            

           //MonthlyAtt table variables
            int present;
            int absent;
            double OT;
            int ShortL;

            //Extra
            int ExPresent = 0;
            int ExAbsent = 0;


            //Incremted Variables
            int Inpresent;
            int Inabsent;
            double InOT;
            int InShortLeaves;

            constring.Open();



            for (attno = 0;attno<100;attno++)
            {

                string selqry = "SELECT * FROM Attendance where AttNO = '" + attno + "' ";
                SqlCommand selcmd = new SqlCommand(selqry, constring);
                SqlDataReader reader = selcmd.ExecuteReader();


                if (reader.Read())
                {

                    //attendance table
                    EmpID = reader["EMPID"].ToString();
                    Attended = reader["Attended"].ToString();
                    attShortL = Convert.ToInt32(reader["ShortL"]);
                    attOT = Convert.ToDouble(reader["OT"]);



                    reader.Close();


                    string seltoUpqry = "SELECT * FROM MonthlyAtt where EMPID = '" + EmpID + "' ";
                    SqlCommand seltoUpcmd = new SqlCommand(seltoUpqry, constring);
                    SqlDataReader ra = seltoUpcmd.ExecuteReader();


                    ra.Read();
                    //MonthlyAtt
                   
                    present = Convert.ToInt32(ra["PresentDays"]);
                    absent = Convert.ToInt32(ra["AbsentDays"]);
                    OT = Convert.ToDouble(ra["OT"]);
                    ShortL = Convert.ToInt32(ra["ShortLeaves"]);

                    ra.Close();

                    if(Attended == "Present   ")   
                    {
                        ExPresent = 1;
                    }
                    else
                    {
                        ExAbsent = 1;
                    }

                    Inpresent = present + ExPresent;
                    Inabsent = absent + ExAbsent;
                    InOT = attOT + OT;
                    InShortLeaves = attShortL + ShortL;

                    string attLeaveqry = "UPDATE MonthlyAtt SET PresentDays = '" + Inpresent + "', AbsentDays = '"+ Inabsent + "', OT = '" + InOT + "', ShortLeaves = '" + InShortLeaves + "'  WHERE EMPID='" + EmpID + "'";
                    SqlCommand cmdLeave = new SqlCommand(attLeaveqry, constring);
                    cmdLeave.ExecuteNonQuery();

                    //constring.Close();

                    

                }
                
             reader.Close();
             
                
                


            }

            for (attno = 0; attno < 100; attno++)
            {

                string attresetqry = "UPDATE Attendance SET Attended = 'Absent', Arrive = '', Leave = '', Mark = '0', ShortL = '0', Minutes = '0', OT = '0'  WHERE AttNO='" + attno + "'";
                SqlCommand cmdattreset = new SqlCommand(attresetqry, constring);
                cmdattreset.ExecuteNonQuery();


            }


             MessageBox.Show("Attendance Collected Sucssesfully and Ready to Next Day...!!!");


            constring.Close();


        }
    }
}
