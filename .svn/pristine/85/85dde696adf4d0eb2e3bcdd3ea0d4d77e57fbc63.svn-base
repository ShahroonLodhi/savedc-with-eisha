using System;
using System.Web.UI;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class SendSMSToDonor : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // page validation
                var oValidator = new Validator();
                oValidator.ValidateRequest(Request);
                oValidator.ValidateUserPageAccess(SaveDCSession.UserAccessLevel,
                                                  new[]
                                                      {
                                                          UserAccessLevels.SuperAdmin, UserAccessLevels.Admin
                                                      });


                int nDonorId = Utils.fixNullInt(Request.QueryString["DonorId"]);

                var user = new User();
                user.UserID = nDonorId;
                var userManager = new UserManager(user);
                user = userManager.Load();

                hdnDonorName.Value = (user.UserName.ToLower().ToString() != "misc")? user.UserName : "donor";
                hdnDonorId.Value = user.UserID.ToString();
                txtPhoneNum.Text = user.PhoneNumber;

                var oCommon = new Common();
                decimal DonorsBalance = Utils.fixNullDecimal(oCommon.GetDonorsBalance(nDonorId));

                txtBalanceHidden.Value = DonorsBalance.ToString() + ".00";
                if (DonorsBalance > 0)
                    txtBalance.Text = "<font color = 'green'>" + DonorsBalance.ToString() + ".00" + "</font>";
                else if (DonorsBalance < 0)
                    txtBalance.Text = "<font color = 'red'>" + DonorsBalance.ToString() + ".00" + "</font>";
                else
                    txtBalance.Text = DonorsBalance.ToString() + ".00";
            }
        }

        protected void btnIncludeBalance_Click(object sender, EventArgs e)
        {
            if (includeBalance.Checked)
                txtMessage.Text = txtMessage.Text + "Your current balance is " + txtBalanceHidden.Value;
            else
                txtMessage.Text = txtMessage.Text.Replace("Your current balance is " + txtBalanceHidden.Value, "");
        }

        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string errorCode = "";
            int donorId = Utils.fixNullInt(hdnDonorId.Value);

            var commom = new Common();
            int statusCode = commom.SendSMSAndLogInDatabase(donorId, hdnDonorName.Value + "," + txtPhoneNum.Text,
                                                            txtMessage.Text, "DON");

            if (statusCode > 0)
            {
                // success.
                errorCode = "5091001";
            }
            else if (statusCode == -1)
            {
                // auth failure
                errorCode = "5091003";
            }
            else if (statusCode == -2)
            {
                // invalid xml
                errorCode = "5091004";
            }
            else if (statusCode == -3)
            {
                // in-suficient balance.
                errorCode = "5091002";
            }
            else if (statusCode == -4)
            {
                // invalid or no recipient
                errorCode = "5091005";
            }
            else
            {
                // unknow error while sending sms.
                errorCode = "5091000";
            }

            string sReturn = Utils.fixNullString(Request.QueryString["return"]);
            bool bReturn = !Convert.ToBoolean(string.Compare(sReturn, "reports", true));

            if (bReturn)
                Response.Redirect("GetDonationReport.aspx?status=" + errorCode); //  fund updated successfully.
            else
                Response.Redirect("ListDonors.aspx?status=" + errorCode); //  fund updated successfully.
        }
    }
}