using System;
using System.Web.UI;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class SendEmailToMember : Page
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
                                                          UserAccessLevels.SuperAdmin, UserAccessLevels.Admin, UserAccessLevels.Operator
                                                      });


                int nDonorId = Utils.fixNullInt(Request.QueryString["MemberId"]);

                if (nDonorId != 0)
                {
                    var user = new User();
                    user.UserID = nDonorId;
                    var userManager = new UserManager(user);
                    user = userManager.Load();

                    hdnDonorName.Value = user.FirstName;
                    hdnDonorId.Value = user.UserID.ToString();
                    txtEmailAddress.Text = user.EmailAddress;
                }
                else
                {
                    hdnDonorName.Value = "Member";
                    txtEmailAddress.Text = "Send Email to all Members";
                    txtEmailAddress.ReadOnly = true;
                }
            }
        }

        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            int donorId = Utils.fixNullInt(hdnDonorId.Value);

            if (donorId != 0)
            {
                var user = new User();
                user.UserID = donorId;
                var userManager = new UserManager(user);
                user = userManager.Load();

                string sReturn = Utils.fixNullString(Request.QueryString["return"]);
                bool bReturn = !Convert.ToBoolean(string.Compare(sReturn, "reports", true));

                if (EmailSender.SendEmailExM(Utils.fixNullString(txtEmailFrom.Text), donorId, new EmailEx
                {
                    DonorName = Utils.fixNullString(user.FirstName),
                    DonorEmail = Utils.fixNullString(txtEmailAddress.Text),
                    CC = Utils.fixNullString(txtCC.Text),
                    Subject = Utils.fixNullString(txtSubject.Text),
                    Body = txtMessage.Text
                }, true))
                    if (bReturn)
                        Response.Redirect("GetDonationReport.aspx?status=5101001"); //  email sent successfully.
                    else
                        Response.Redirect("ListMembers.aspx?status=5101001"); //  email sent successfully.
                else
                    if (bReturn)
                        Response.Redirect("GetDonationReport.aspx?status=5101000"); //  email not sent.
                    else
                        Response.Redirect("ListMembers.aspx?status=5101000"); //  email not sent.
            }
            else
            {
                if (EmailSender.SendEmailExM(Utils.fixNullString(txtEmailFrom.Text), donorId, new EmailEx
                {
                    DonorName = "Member",
                    DonorEmail = "accounts@save-dc.org",
                    CC = Utils.fixNullString(txtCC.Text),
                    BCC = true,
                    Subject = Utils.fixNullString(txtSubject.Text),
                    Body = txtMessage.Text
                }, true))
                    Response.Redirect("ListMembers.aspx?status=5101001"); //  email sent successfully.
                else
                    Response.Redirect("ListMembers.aspx?status=5101000"); //  email not sent.
            }
        }
    }
}