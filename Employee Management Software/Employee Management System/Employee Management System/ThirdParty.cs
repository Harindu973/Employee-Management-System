using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Employee_Management_System
{
    class ThirdParty
    {
        public bool CheckAttend(string QRval)
        {
            SqlConnection constring = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='E:\C# Databases\EMP.mdf';Integrated Security=True;Connect Timeout=30");
            var time = DateTime.Now;
            bool result = false;
            constring.Open();

            

            string selqry = "SELECT * FROM Attendance where EMPID = '" + QRval + "' ";
            SqlCommand selcmd = new SqlCommand(selqry, constring);
            SqlDataReader reader = selcmd.ExecuteReader();
            reader.Read();

            string att = reader["Attended"].ToString();
         
            reader.Close();


             if (att == "Present   ")
             {
                string attLeaveqry = "UPDATE Attendance SET Leave = '" + time + "' WHERE EMPID='" + QRval + "'";
                SqlCommand cmdLeave = new SqlCommand(attLeaveqry, constring);
                cmdLeave.ExecuteNonQuery();
                result = true;
                constring.Close();
                return result;
            }
            else
            {
                string attArriveqry = "UPDATE Attendance SET Arrive = '" + time + "' WHERE EMPID='" + QRval + "'";
                SqlCommand cmdArrive = new SqlCommand(attArriveqry, constring);
                cmdArrive.ExecuteNonQuery();
                constring.Close();
                return result;
            }
            
            


  


        }

    }
}
