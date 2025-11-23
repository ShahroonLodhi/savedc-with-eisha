using System;
using System.Configuration;
using System.IO;

namespace SaveDC.ControlPanel.Src.Configurations
{
    public static class EmailConfiguration
    {
        private static readonly bool _sendReminders;
        private static readonly string _smtpHost;
        private static readonly int _smtpPort;
        private static readonly string _fromAdress_info;
        private static readonly string _fromPwd_info;
        private static readonly string _fromAdress_accounts;
        private static readonly string _fromPwd_accounts;
        private static readonly string _subject;
        private static readonly string _mailbody;

        static EmailConfiguration()
        {
            try
            {
                _sendReminders = Convert.ToBoolean(ReadConfigKey("MailSendReminders"));
                _smtpHost = ReadConfigKey("MailSMTPHost");
                _smtpPort = Convert.ToInt32(ReadConfigKey("MailSMTPPort"));
                _fromAdress_info = ReadConfigKey("MailFromAddressInfo");
                _fromPwd_info = ReadConfigKey("MailFromPwdInfo");
                _fromAdress_accounts = ReadConfigKey("MailFromAddressAccounts");
                _fromPwd_accounts = ReadConfigKey("MailFromPwdAccount");
                _subject = ReadConfigKey("MailSubject");
                _mailbody = File.ReadAllText(ReadConfigKey("MailBodyPath"));
            }
            catch
            {
            }
        }

        public static bool SendReminders
        {
            get { return _sendReminders; }
        }
        
        public static string SMTPHost
        {
            get { return _smtpHost; }
        }

        public static int SMTPPort
        {
            get { return _smtpPort; }
        }

        public static string FromAddressInfo
        {
            get { return _fromAdress_info; }
        }

        public static string FromPwdInfo
        {
            get { return _fromPwd_info; }
        }

        public static string FromAddressAccounts
        {
            get { return _fromAdress_accounts; }
        }

        public static string FromPwdAccounts
        {
            get { return _fromPwd_accounts; }
        }

        public static string MailSubject
        {
            get { return _subject; }
        }

        public static string MailBody
        {
            get { return _mailbody; }
        }

        public static string ReadConfigKey(string szKey)
        {
            try
            {
                string szKeyValue = "";
                if (ConfigurationManager.AppSettings[szKey] == null || ConfigurationManager.AppSettings[szKey] == "")
                    szKeyValue = null;
                else
                    szKeyValue = Convert.ToString(ConfigurationManager.AppSettings[szKey]);

                return szKeyValue;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}