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
            var arriveT = Convert.ToDateTime(reader["Arrive"]);
            var leaveT = Convert.ToDateTime(reader["Leave"]);
            //var leavT = Convert.ToInt32(reader["Leave"]);


            var timespan = leaveT.Subtract(arriveT).TotalMinutes;
           
            reader.Close();


             if (att == "Present   ")
             {
                string attLeaveqry = "UPDATE Attendance SET Leave = '" + time + "' WHERE EMPID='" + QRval + "'";
                SqlCommand cmdLeave = new SqlCommand(attLeaveqry, constring);
                cmdLeave.ExecuteNonQuery();

                string timespanqry = "UPDATE Attendance SET Minutes = '" + timespan + "' WHERE EMPID='" + QRval + "'";
                SqlCommand cmdtimespan = new SqlCommand(timespanqry, constring);
                cmdtimespan.ExecuteNonQuery();

                if (timespan <= 450)
                {
                    string slqry = "UPDATE Attendance SET ShortL = '1' WHERE EMPID='" + QRval + "'";
                    SqlCommand cmdsl = new SqlCommand(slqry, constring);
                    cmdsl.ExecuteNonQuery();
                }

                if(timespan > 480)
                {
                    var Hours = (timespan / 60)-8;
                    //int intHours = int.Parse(Hours.ToString());

                    string otqry = "UPDATE Attendance SET OT = '" + Hours + "' WHERE EMPID='" + QRval + "'";
                    SqlCommand cmdot = new SqlCommand(otqry, constring);
                    cmdot.ExecuteNonQuery();
                }
               




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
