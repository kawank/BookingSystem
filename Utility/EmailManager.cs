using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utility
{
    public class EmailManager
    {
        public bool SendMail(Email email)
        {
            bool isSent = false;
            try
            {
                /*  SmtpClient client = new SmtpClient();
               MailMessage message = new MailMessage();*/

                using (MailMessage mm = new MailMessage(email.FromEmailAddress, email.ToEmailAddressList.FirstOrDefault()))
                {
                    mm.Subject = email.Subject;
                    mm.Body = email.Body;

                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = email.Host;
                    smtp.EnableSsl = true;
                    smtp.Port = email.Port.ToIntSafe();
                    NetworkCredential NetworkCred = new NetworkCredential(email.AccountUsername.Trim(), email.AccountPassword.Trim());
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(mm);

                }

                isSent = true;
                return isSent;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public bool SendMailAsync(Email email)
        {
            try
            {
                Thread emailThread = new Thread(delegate()
                {
                    SendMail(email);
                });

                emailThread.IsBackground = true;
                emailThread.Start();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
