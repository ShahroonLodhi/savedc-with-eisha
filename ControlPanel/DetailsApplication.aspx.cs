using System;
using System.IO;
using System.Web.UI;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class DetailsApplication : Page
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


                // get form/query string values.
                int nEditAppId = Utils.fixNullInt(Request.QueryString["AppId"]);

                // load edit user details.
                if (nEditAppId > 0)
                {
                    Application oApp = new Common().LoadNewApplication(nEditAppId);

                    txtStudentFName.Text = oApp.StdFirstName;
                    txtStudentLName.Text = oApp.StdLastName;
                    txtDOB.Text = oApp.StdDateOfBirth == DateTime.MinValue
                                      ? DateTime.Now.Date.ToString("dd/MM/yyyy")
                                      : oApp.StdDateOfBirth.ToString("dd/MM/yyyy");
                    txtPrevClass.Text = oApp.StdPrevClasses;

                    txtGuardianName.Text = oApp.GuardianName;
                    txtGAddress.Text = oApp.GuardianAddress;

                    txtApplicantName.Text = oApp.ApplicantName;
                    txtApplicantDO.Text = oApp.ApplicantFatherName;
                    txtApplicantContactNum.Text = oApp.ApplicantContactNum;
                    txtReceivedOn.Text = oApp.ReceivedOn == DateTime.MinValue
                                             ? DateTime.Now.Date.ToString("dd/MM/yyyy")
                                             : oApp.ReceivedOn.ToString("dd/MM/yyyy");

                    txtApplicationNum.Text = oApp.ApplicationNum;
                    txtAppCategory.Text = oApp.ApplicationCategory;
                    txtAppReceivedBy.Text = oApp.ReceivedBy;
                    txtReferedBy.Text = oApp.ReferredBy;

                    txtDeliveryNotes.Text = oApp.DeliveryNotes;
                    txtFieldVarificationPerson.Text = oApp.FieldVarificationPerson;
                    txtBNote.Text = oApp.BoardNotes;

                    txtNote.Text = oApp.Notes;

                    string szFileUniqueName = oApp.ImageGuid;
                    string szServerPath = Server.MapPath(SaveDCConstants.ProfileSnapUploadPath) + szFileUniqueName +
                                          ".jpg";
                    if (File.Exists(szServerPath))
                    {
                        imgUpload.Src = SaveDCConstants.ProfileSnapUploadPath + szFileUniqueName + ".jpg";
                    }


                    // cert 1
                    string szViewHTML =
                        "window.open('{0}?modalwin=1', 'View', 'left=100,top=30,screenX=100,screenY=30, height=550,width=840,toolbar=no,directories=no,status=no,menubar=no,modal=yes,scrollbars=yes');";
                    szServerPath = Server.MapPath(SaveDCConstants.BillsUploadPath) + oApp.Cert1GUID + ".jpg";
                    if (File.Exists(szServerPath))
                    {
                        viewCert1.Attributes.Add("onclick",
                                                 string.Format(szViewHTML,
                                                               SaveDCConstants.BillsUploadPath + oApp.Cert1GUID +
                                                               ".jpg"));
                    }
                    else
                    {
                        viewCert1.Parent.Controls.Remove(viewCert1);
                    }
                    // cert 2
                    szServerPath = Server.MapPath(SaveDCConstants.BillsUploadPath) + oApp.Cert2GUID + ".jpg";
                    if (File.Exists(szServerPath))
                    {
                        viewCert2.Attributes.Add("onclick",
                                                 string.Format(szViewHTML,
                                                               SaveDCConstants.BillsUploadPath + oApp.Cert2GUID +
                                                               ".jpg"));
                    }
                    else
                    {
                        viewCert2.Parent.Controls.Remove(viewCert2);
                    }
                }
                hdnEditAppID.Value = nEditAppId.ToString();
                RenderError(Request.QueryString["status"]);
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