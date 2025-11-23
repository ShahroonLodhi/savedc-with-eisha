using System;
using System.Web.UI;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class AdminSettings : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // page validation
            var oValidator = new Validator();
            oValidator.ValidateRequest(Request);
            oValidator.ValidateUserPageAccess(SaveDCSession.UserAccessLevel, new[] {UserAccessLevels.SuperAdmin});
            oValidator = null;

            if (!Page.IsPostBack)
            {
                // get form/query string values.
                var oCommon = new Common();
                string szMinRej = oCommon.GetAdminSettingVal("minrejections");
                string szMinApprov = oCommon.GetAdminSettingVal("minapprovals");

                txtRejections.Text = szMinRej;
                txtApprovals.Text = szMinApprov;

                RenderError(Request.QueryString["status"]);
            }
        }

        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            string szMinRej = txtRejections.Text;
            string szMinApprov = txtApprovals.Text;

            var oCommon = new Common();
            int nStatus1 = oCommon.SaveAdminSettingVal("minrejections", szMinRej);
            int nStatus2 = oCommon.SaveAdminSettingVal("minapprovals", szMinApprov);

            if (nStatus1 > 0 && nStatus2 > 0)
            {
                Response.Redirect("AdminSettings.aspx?status=5060011");
            }
            else
            {
                Response.Redirect("AdminSettings.aspx?status=5060010");
            }
        }

        private void RenderError(string szErrorCode)
        {
            if (string.IsNullOrEmpty(szErrorCode))
            {
                lblError.Text = "";
                return;
            }

            string szErrorDesc = Utils.GetMessageText(szErrorCode);
            if (!szErrorCode.EndsWith("1"))
                lblError.CssClass = "FailureMessage";
            else
                lblError.CssClass = "SuccessMessage";

            lblError.Text = szErrorDesc;
        }
    }
}