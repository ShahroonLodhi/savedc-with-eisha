using System;
using System.Web.UI;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class SendEmailToSchool : Page
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
                                                          UserAccessLevels.SuperAdmin, UserAccessLevels.Admin,
                                                          UserAccessLevels.Operator
                                                      });


                int nSchoolId = Utils.fixNullInt(Request.QueryString["SchoolId"]);

                if (nSchoolId != 0)
                {
                    var user = new School();
                    user.SchoolID = nSchoolId;
                    var userManager = new SchoolManager(user);
                    user = userManager.Load();

                    hdnSchoolName.Value = (user.SchoolName.ToLower().ToString() != "misc") ? user.SchoolName : "school";
                    hdnSchoolId.Value = user.SchoolID.ToString();
                    txtEmailAddress.Text = user.EmailAddress;

                }
                else
                {
                    hdnSchoolName.Value = "School";
                    txtEmailAddress.Text = "Send Email to all Schools";
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

            int schoolID = Utils.fixNullInt(hdnSchoolId.Value);

            if (schoolID != 0)
            {
                var user = new School();
                user.SchoolID = schoolID;
                var userManager = new SchoolManager(user);
                user = userManager.Load();

                if (EmailSender.SendEmailEx(Utils.fixNullString(txtEmailFrom.Text), schoolID, new EmailEx
                {
                    DonorName = Utils.fixNullString(user.SchoolName),
                    DonorEmail = Utils.fixNullString(txtEmailAddress.Text),
                    CC = Utils.fixNullString(txtCC.Text),
                    Subject = Utils.fixNullString(txtSubject.Text),
                    Body = txtMessage.Text
                }, true))
                    Response.Redirect("ListSchools.aspx?status=5101001"); //  email sent successfully.
                
                else
                    Response.Redirect("ListSchools.aspx?status=5101000"); //  email not sent.
            }
            else
            {
                if (EmailSender.SendEmailEx(Utils.fixNullString(txtEmailFrom.Text), schoolID, new EmailEx
                {
                    DonorName = "School",
                    DonorEmail = "accounts@save-dc.org",
                    CC = Utils.fixNullString(txtCC.Text),
                    BCC = true,
                    Subject = Utils.fixNullString(txtSubject.Text),
                    Body = txtMessage.Text
                }, true))
                    Response.Redirect("ListSchools.aspx?status=5101001"); //  email sent successfully.
                else
                    Response.Redirect("ListSchools.aspx?status=5101000"); //  email not sent.
            }
        }
    }
}