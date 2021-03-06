﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace DentalSurgery.Services
{
    public class EmailSender
    {
        private static readonly string _ownerEmail = "maciekgasiorek9@gmail.com";
        private static readonly string _surgeryEmail = "dentalsurgery429@gmail.com";
        private static readonly string _fromPassword = "haslomaslo1!";
        public static void SendNewOpinionNotification(string userName, string opinionText)
        {
            var fromAddress = new MailAddress(_surgeryEmail);
            var toAddress = new MailAddress(_ownerEmail);

            string subject = "Napisano nową opinię";
            string body = $"Użytkownik {userName} napisał nową opinię:\n{opinionText}";

            Send(fromAddress, toAddress, _fromPassword, subject, body);
        }
        public static void SendDefaultPassword(string userEmail, string password)
        {
            var fromAddress = new MailAddress(_surgeryEmail);
            var toAddress = new MailAddress(_ownerEmail);

            string subject = "Zostałeś zarejestrowany w gabinecie dentystycznym";
            string body = $"Zostałeś zarejestrowany w gabinecie dentystycznym.\nMożesz zalogować się używając emaila: {userEmail} oraz hasła: {password}";

            Send(fromAddress, toAddress, _fromPassword, subject, body);
        }
        public static void SendVisitsSchedule(string filePath)
        {
            var fromAddress = new MailAddress(_surgeryEmail);
            var toAddress = new MailAddress(_ownerEmail);
            string subject = "Harmonogram wizyt";
            string body = "Wygenerowano harmonogram wizyt";
            var attachment = new Attachment(filePath);

            SendWithAttachment(fromAddress, toAddress, _fromPassword, subject, body, attachment, filePath);
        }
        public static void Send(MailAddress fromAddress, MailAddress toAddress, string fromPassword, string subject, string body)
        {
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

            };
            var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };
            smtp.Send(message);
        }
        public static void SendWithAttachment(MailAddress fromAddress, MailAddress toAddress, string fromPassword, string subject, string body, Attachment attachment, string filePath)
        {
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

            };
            var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };
            message.Attachments.Add(attachment);
            smtp.Send(message);
        }
    }
}