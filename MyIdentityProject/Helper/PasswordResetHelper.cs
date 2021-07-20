using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using MyIdentityProject.Models;
using System.Net;

namespace MyIdentityProject.Helper
{
    public static class PasswordResetHelper
    {
        public static void PasswordResetSendEmail(string link,AppUser user)   
        {
            
            SmtpClient smptclient = new SmtpClient();
            var fromAddress = new MailAddress("fatih.avsar.fb@gmail.com","Fatih Avs");
            var toAddress = new MailAddress(user.Email, user.UserName);
            const string fromPassword = "tektabanca1.";
            const string subject = "Sifre Yenileme İslemleri";
            string body =  $"<p>{user.UserName} Merhaba.Aşağıdaki bağlantıya tıklayarak şifrenizi yenileyebilirsiniz</p></br> {link} !!";
            smptclient.Host = "smtp.gmail.com";
            smptclient.Port = 587;
            smptclient.EnableSsl = true;
            smptclient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smptclient.Credentials = new NetworkCredential(fromAddress.Address, fromPassword);
            smptclient.Timeout = 20000;
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
                
            })
            
            {
                smptclient.Send(message);
            }

        }

    }
}
