using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Employee_Management_System
{
    public class Email
    {
        public void email()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("181plymouthsoc@gmail.com");
                mail.To.Add("dakokum@gmail.com");

                mail.Subject = "Attendence sheet";

                //body

                mail.Body = "body";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("181plymouthsoc@gmail.com", "123456789soc");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                //MessageBox.Show("mail Send");
            }
            catch (Exception)
            {
               // MessageBox.Show(ex.ToString());
            }


        }
        
    }
}
