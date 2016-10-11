using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EcoTrend_Industry_Project.BusinessLogic;
using EcoTrend_Industry_Project.ViewModels;
using System.Net.Mail;

namespace EcoTrend_Industry_Project.BusinessLogic
{
    public class MailHelper
    {
        public const string SUCCESS
= "Success! Your email has been sent.  Please allow up to 48 hrs for a reply.";
        string to; // Specify where you want this email sent.
                   // This value may/may not be constant.
                   // To get started use one of your email 
                   // addresses.
        public string EmailFromArvixe(Message message)
        {
            to = message.To;
            // Use credentials of the Mail account that you created with the steps above.
            //string FROM = message.Sender.ToString();
            string FROM = "ecotrend.noreply@ahui09.com";
            const string FROM_PWD = "ecotrend123";

            //const string FROM = "emma@emmavilatajdeveloper.com";

            const bool USE_HTML = true;

            // Get the mail server obtained in the steps described above.
            const string SMTP_SERVER = "143.95.249.35";
            try
            {
                MailMessage mailMsg = new MailMessage(FROM, to);
                mailMsg.Subject = message.Subject;
                mailMsg.Body = message.Body + "<br/>sent by: " + message.Sender;
                mailMsg.IsBodyHtml = USE_HTML;

                SmtpClient smtp = new SmtpClient();
                smtp.Port = 25;
                smtp.Host = SMTP_SERVER;
                smtp.Credentials = new System.Net.NetworkCredential(FROM, FROM_PWD);
                smtp.Send(mailMsg);
            }
            catch (System.Exception ex)
            {
                return ex.Message + "Email has not been send to" + message.To.ToString();
            }
            return SUCCESS;
        }

    }
}