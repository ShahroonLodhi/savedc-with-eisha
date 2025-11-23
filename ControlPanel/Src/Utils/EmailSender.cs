using System;
using System.Net;
using System.Net.Mail;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.DB;
using System.Data;
using System.Data.SqlClient;

namespace SaveDC.ControlPanel.Src.Utils
{
    public class EmailSender
    {
        public static bool SendEmail(string emailFrom, int UserId, Email email)
        {
            try
            {
                var fromAddress = new MailAddress(emailFrom);
                var toAddress = new MailAddress(email.DonorEmail);
                string month = DateTime.Now.ToString("MMM, yyyy");

                string body = string.Format(EmailConfiguration.MailBody, email.FirstName + " " + email.LastName,
                                            email.Amount, month);

                string _fromAddress, _fromPwd;
                if (emailFrom.Contains("info"))
                {
                    _fromAddress = EmailConfiguration.FromAddressInfo;
                    _fromPwd = EmailConfiguration.FromPwdInfo;
                }
                else if (emailFrom.Contains("accounts"))
                {
                    _fromAddress = EmailConfiguration.FromAddressAccounts;
                    _fromPwd = EmailConfiguration.FromPwdAccounts;
                }
                else
                {
                    _fromAddress = "accounts@save-dc.org";
                    _fromPwd = "Azharmoon@12345";
                }

                var smtp = new SmtpClient
                               {
                                   Host = EmailConfiguration.SMTPHost,
                                   Port = EmailConfiguration.SMTPPort,
                                   EnableSsl = true,
                                   DeliveryMethod = SmtpDeliveryMethod.Network,
                                   Credentials = new NetworkCredential(_fromAddress, _fromPwd),
                                   Timeout = 20000
                               };
                using (var message = new MailMessage(fromAddress, toAddress)
                                         {
                                             Subject = EmailConfiguration.MailSubject,
                                             Body = body
                                         })
                {
                    smtp.Send(message);
                    LogEmail(UserId, "", message.Subject, message.Body);
                }
            }
            catch (Exception e)
            {
                Utils.LogErrorToFile(e);
                return false;
            }
            return true;
        }

        private static void LogEmail(int UserId, string cc, string subject, string body)
        {
            // add in db
            var dbmanager = new DBManager();
            SqlParameter[] parameters = {
                                            dbmanager.makeInParam("@UserId", SqlDbType.Int, 0, UserId),
                                            dbmanager.makeInParam("@CC", SqlDbType.NVarChar, 255,
                                                                  cc),
                                            dbmanager.makeInParam("@Subject", SqlDbType.NVarChar, 50,
                                                                  subject),
                                            dbmanager.makeInParam("@Body", SqlDbType.NVarChar, 2000,
                                                                  body)
                                        };
            dbmanager.RunProc("LogSentEmail", parameters);
        }

        public static bool SendEmailEx(string emailFrom, int UserId, EmailEx email, bool changeBody)
        {
            try
            {
                var fromAddress = new MailAddress(emailFrom);
                var toAddress = new MailAddress(email.DonorEmail);

                string _fromAddress, _fromPwd;
                if (emailFrom.Contains("info"))
                { 
                    _fromAddress = EmailConfiguration.FromAddressInfo;
                    _fromPwd = EmailConfiguration.FromPwdInfo;
                }
                else if (emailFrom.Contains("accounts"))
                {
                    _fromAddress = EmailConfiguration.FromAddressAccounts;
                    _fromPwd = EmailConfiguration.FromPwdAccounts;
                }
                else
                {
                    _fromAddress = "accounts@save-dc.org";
                    _fromPwd = "Azharmoon@12345";
                }
                
                var smtp = new SmtpClient
                {
                    Host = EmailConfiguration.SMTPHost,
                    Port = EmailConfiguration.SMTPPort,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(_fromAddress, _fromPwd),
                    Timeout = 20000
                };
                using (var message = new MailMessage(new MailAddress(_fromAddress), toAddress)
                {
                    Subject = email.Subject,
                    Body = (changeBody) ? "Dear " + email.DonorName + ",\r\n" + email.Body + "\r\n\r\nRegards,\r\nSAVE DC Email Service\r\nhttp://save-dc.org" : email.Body
                })
                {
                    if (email.CC != "" && email.CC != null)
                    {
                        var ccAddress = new MailAddress(email.CC);
                        message.CC.Add(ccAddress);
                    }
                    if (email.BCC == true)
                    {
                        var oUser = new User();
                        var oUserManager = new UserManager(oUser);
                        oUser.UserRoleID = 4;
                        User[] users = oUserManager.GetUsers();
                        
                        for (var i = 0; i < users.Length; i++)
                        {
                            if (users[i].EmailAddress != "" && users[i].EmailAddress != null && users[i].EmailAddress.Contains("@"))
                            {
                                var bccAddress = new MailAddress(users[i].EmailAddress);
                                message.Bcc.Add(bccAddress);
                            }
                        }
                    }
                    smtp.Send(message);
                    LogEmail(UserId, message.CC.ToString(), message.Subject, message.Body);
                }
            }
            catch (Exception e)
            {
                Utils.LogErrorToFile(e);
                return false;
            }
            return true;
        }

        public static bool SendEmailExM(string emailFrom, int UserId, EmailEx email, bool changeBody)
        {
            try
            {
                var fromAddress = new MailAddress(emailFrom);
                var toAddress = new MailAddress(email.DonorEmail);

                string _fromAddress, _fromPwd;
                if (emailFrom.Contains("info"))
                {
                    _fromAddress = EmailConfiguration.FromAddressInfo;
                    _fromPwd = EmailConfiguration.FromPwdInfo;
                }
                else if (emailFrom.Contains("accounts"))
                {
                    _fromAddress = EmailConfiguration.FromAddressAccounts;
                    _fromPwd = EmailConfiguration.FromPwdAccounts;
                }
                else
                {
                    _fromAddress = "accounts@save-dc.org";
                    _fromPwd = "Azharmoon@12345";
                }

                var smtp = new SmtpClient
                {
                    Host = EmailConfiguration.SMTPHost,
                    Port = EmailConfiguration.SMTPPort,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(_fromAddress, _fromPwd),
                    Timeout = 20000
                };
                using (var message = new MailMessage(new MailAddress(_fromAddress), toAddress)
                {
                    Subject = email.Subject,
                    Body = (changeBody) ? "Dear " + email.DonorName + ",\r\n" + email.Body + "\r\n\r\nRegards,\r\nSAVE DC Email Service\r\nhttp://save-dc.org" : email.Body
                })
                {
                    if (email.CC != "" && email.CC != null)
                    {
                        var ccAddress = new MailAddress(email.CC);
                        message.CC.Add(ccAddress);
                    }
                    if (email.BCC == true)
                    {
                        var oUser = new User();
                        var oUserManager = new UserManager(oUser);
                        oUser.UserRoleID = 5;
                        User[] users = oUserManager.GetUsers();

                        for (var i = 0; i < users.Length; i++)
                        {
                            if (users[i].EmailAddress != "" && users[i].EmailAddress != null && users[i].EmailAddress.Contains("@"))
                            {
                                var bccAddress = new MailAddress(users[i].EmailAddress);
                                message.Bcc.Add(bccAddress);
                            }
                        }
                    }
                    smtp.Send(message);
                    LogEmail(UserId, message.CC.ToString(), message.Subject, message.Body);
                }
            }
            catch (Exception e)
            {
                Utils.LogErrorToFile(e);
                return false;
            }
            return true;
        }
    }
}