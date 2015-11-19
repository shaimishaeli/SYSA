using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace MeetingSummary.Models
{
    public static class MailSender
    {
        public static void SendMail(Users toUser, string subject, string body)
        {
            var fromAddress = new MailAddress("memomedapp@gmail.com", "memomedapp");
            var toAddress = new MailAddress(toUser.Email, toUser.Name);
            const string fromPassword = "memomedapp2015";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}