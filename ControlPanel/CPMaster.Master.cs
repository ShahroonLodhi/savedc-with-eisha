using System;
using System.Web;
using System.Web.UI;
using SaveDC.ControlPanel.Src.Configurations;

namespace SaveDC.ControlPanel
{
    public partial class CPMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext incoming = HttpContext.Current;
            string oldpath = incoming.Request.Path.ToLower();

            if (SaveDCSession.UserId < 1)
                Response.Redirect("Login.aspx");


            lblUserName.Text = SaveDCSession.UserName;
            lblRoleName.Text = SaveDCSession.UserRoleName;
        }
    }
}