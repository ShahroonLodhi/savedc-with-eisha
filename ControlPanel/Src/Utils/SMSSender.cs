using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using SaveDC.ControlPanel.Src.Configurations;

namespace SaveDC.ControlPanel.Src.Utils
{
    public class SMSRecipient
    {
        public string Name { set; get; }
        public string PhoneNum { set; get; }
    }

    public class SMSSender
    {
        private readonly String _ApiUserName;
        private readonly String _ApiUserPassword;
        private readonly String _SenderName;
        private readonly String _XMLPacketToSend;

        private SMSSender()
        {
            _ApiUserName = "haider.01@gmail.com";
            _ApiUserPassword = "idontknow";
            _SenderName = EmailConfiguration.ReadConfigKey("SMSSender");
            _XMLPacketToSend = "";
        }

        public SMSSender(String message, List<SMSRecipient> recipientsList)
            : this()
        {
            if (recipientsList.Count == 1 && !string.IsNullOrEmpty(recipientsList[0].Name))
            {
                message = "Dear " + recipientsList[0].Name + ", " + message;
            }
            else
            {
                string textFromFile = EmailConfiguration.ReadConfigKey("MarkitingSMSTop");
                message = textFromFile + ", " + message;
            }

            // append message footer.
            string msgFotter = EmailConfiguration.ReadConfigKey("MarkitingSMSBottom");
            message = message + "\r\n" + msgFotter;

            _XMLPacketToSend = GetRequestXMLPacket(message, recipientsList);
        }

        public Int16 SendSMS()
        {
            Int16 statusCode = 0;
            string response = SendRequestToService();

            if (!String.IsNullOrEmpty(response))
            {
                var responseDoc = new XmlDocument();
                responseDoc.LoadXml(response);
                XmlNode selectSingleNode = responseDoc.SelectSingleNode("//status");
                if (selectSingleNode != null)
                {
                    statusCode = Convert.ToInt16(selectSingleNode.InnerText);
                }
            }
            return statusCode;
        }

        private String GetRequestXMLPacket(String message, List<SMSRecipient> recipientsList)
        {
            const String xmlPacket =
                "<SMS><operations><operation>SEND</operation></operations><authentification><username>{0}</username><password>{1}</password>" +
                "</authentification><message><sender>{2}</sender><text>{3}</text></message><numbers>{4}</numbers></SMS>";
            const String recipientsXmlPacket = "<number>{0}</number>";

            String recipientsPacket = "";
            foreach (SMSRecipient recipient in recipientsList)
            {
                // validate recipient number.
                recipientsPacket += String.Format(recipientsXmlPacket, recipient.PhoneNum);
            }

            return String.Format(xmlPacket, _ApiUserName, _ApiUserPassword, _SenderName,
                                 message, recipientsPacket);
        }

        private String SendRequestToService()
        {
            string _url = "http://atompark.com/members/sms/xml.php";
            string _action = "http://atompark.com/members/sms/xml.php?op=XML";

            XmlDocument soapEnvelopeXml = CreateSoapEnvelope();
            HttpWebRequest webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            String soapResult;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            using (var rd = new StreamReader(webResponse.GetResponseStream()))
            {
                soapResult = rd.ReadToEnd();
            }
            return soapResult;
        }

        private HttpWebRequest CreateWebRequest(String url, String action)
        {
            var webRequest = (HttpWebRequest) WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private XmlDocument CreateSoapEnvelope()
        {
            var soapEnvelop = new XmlDocument();
            soapEnvelop.LoadXml(
                @"<SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/1999/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/1999/XMLSchema""><SOAP-ENV:Body>" +
                _XMLPacketToSend + "</SOAP-ENV:Body></SOAP-ENV:Envelope>");
            return soapEnvelop;
        }

        private void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
    }
}