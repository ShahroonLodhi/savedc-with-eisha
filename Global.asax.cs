using System;
using System.Web;

namespace SaveDC
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            //if (Server.GetLastError() != null)
            //{
            //    string ErrorMessage = Server.GetLastError().Message;
            //    if (ErrorMessage.ToUpper() != "FILE DOES NOT EXIST.")
            //    {
            //        string ErrorSource = Server.GetLastError().Source;
            //        string filePath = "/ErrorsParking.aspx?Message=" + ErrorMessage + "&Source=" + ErrorSource;
            //        Server.Transfer(filePath);
            //    }
            //}
        }

        protected void OnAcquireRequestState(Object sender, EventArgs e)
        {
            //Response.AddHeader("Pragma", "no-cache");
            //Response.CacheControl = "no-cache";
            //Response.ExpiresAbsolute = System.DateTime.Now.AddDays(-5);
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            //HttpContext incoming = HttpContext.Current;
            //string oldpath = incoming.Request.Path.ToLower();

            //if (oldpath.IndexOf("Login.aspx") > -1)
            //{
            //    return;
            //}

            //if(SaveDC.ControlPanel.Src.Configurations.SESSION.UserId  < 1)
            //    Server.Transfer("Login.aspx");
        }
    }
}