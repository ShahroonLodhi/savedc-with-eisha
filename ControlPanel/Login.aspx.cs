using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["autologin"]))
                {
                    // validate user.
                    var oUserAuto = new User();
                    oUserAuto.UserID = Convert.ToInt32(Request.QueryString["autologin"]);
                    //oUser.UserPassword = szLoginPassword;

                    var oUserAutoManager = new UserManager(oUserAuto);
                    oUserAuto = oUserAutoManager.Load();

                    {
                        // validate user.
                        var oUser = new User();
                        oUser.UserName = oUserAuto.UserName;
                        oUser.UserPassword = oUserAuto.UserPassword;

                        var oUserManager = new UserManager(oUser);
                        bool bIsValidUser = oUserManager.Login();

                        if (bIsValidUser)
                        {
                            SaveDCSession.AutoLogin = true;
                            SaveDCSession.AutoUserId = SaveDCSession.UserId;

                            oUserManager.LoadSession();
                            FormsAuthentication.SetAuthCookie(HttpContext.Current.Session["UserName"].ToString(), false, "/");
                            Response.Redirect("CPHome.aspx");
                        }
                        else
                        {
                            RenderError("5011280");
                        }

                    }
                }
                else if (SaveDCSession.AutoLogin)
                {
                    // validate user.
                    var oUserAuto = new User();
                    oUserAuto.UserID = SaveDCSession.AutoUserId;
                    //oUser.UserPassword = szLoginPassword;

                    SaveDCSession.AutoLogin = false;
                    SaveDCSession.AutoUserId = 0;

                    var oUserAutoManager = new UserManager(oUserAuto);
                    oUserAuto = oUserAutoManager.Load();

                    {
                        // validate user.
                        var oUser = new User();
                        oUser.UserName = oUserAuto.UserName;
                        oUser.UserPassword = oUserAuto.UserPassword;

                        var oUserManager = new UserManager(oUser);
                        bool bIsValidUser = oUserManager.Login();

                        if (bIsValidUser)
                        {
                            oUserManager.LoadSession();
                            FormsAuthentication.SetAuthCookie(HttpContext.Current.Session["UserName"].ToString(), false, "/");
                            Response.Redirect("CPHome.aspx");
                        }
                        else
                        {
                            RenderError("5011280");
                        }

                    }
                }

                Session.Clear();
                loginName.Focus();

                RenderError(Request.QueryString["status"]);
            }
        }

        private void RenderError(string szErrorCode)
        {
            if (string.IsNullOrEmpty(szErrorCode))
            {
                lblMessage.Text = "";
                return;
            }

            string szErrorDesc = Utils.GetMessageText(szErrorCode);
            if (!szErrorCode.EndsWith("1"))
                lblMessage.CssClass = "FailureMessage";
            else
                lblMessage.CssClass = "SuccessMessage";

            lblMessage.Text = szErrorDesc;
        }


        protected void btnLogin_Click(object sender, ImageClickEventArgs e)
        {
            string szLoginName = loginName.Text;
            string szLoginPassword = loginPassword.Text;

            // validate request.

            // validate user.
            var oUser = new User();
            oUser.UserName = szLoginName;
            oUser.UserPassword = szLoginPassword;

            var oUserManager = new UserManager(oUser);
            bool bIsValidUser = oUserManager.Login();

            if (bIsValidUser)
            {
                oUserManager.LoadSession();
                FormsAuthentication.SetAuthCookie(HttpContext.Current.Session["UserName"].ToString(), false, "/");
                Response.Redirect("CPHome.aspx");
            }
            else
            {
                RenderError("5011280");
            }
        }
    }
}