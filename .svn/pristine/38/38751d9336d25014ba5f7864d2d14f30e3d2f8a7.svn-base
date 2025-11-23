using System;
using System.IO;
using System.Web;
using System.Web.UI;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class AddApplication : Page
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
                ;

                // dont allow operator to edit.
                if (SaveDCSession.UserAccessLevel != UserAccessLevels.Operator)
                {
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
                        rbAppCategory.SelectedValue = oApp.ApplicationCategory;
                        rbAppReceivedBy.SelectedValue = oApp.ReceivedBy;
                        txtReferedBy.Text = oApp.ReferredBy;

                        txtDeliveryNotes.Text = oApp.DeliveryNotes;
                        txtFieldVarificationPerson.Text = oApp.FieldVarificationPerson;
                        txtBNote.Text = oApp.BoardNotes;

                        hdnProfileImage.Value = oApp.ImageGuid;
                        txtNote.Text = oApp.Notes;

                        SetCertiInformation(oApp.Cert1GUID, "1");
                        SetCertiInformation(oApp.Cert2GUID, "2");


                        hdnEditAppID.Value = nEditAppId.ToString();
                        hdnAddEdit.Value = "Edit";

                        string szFileUniqueName = oApp.ImageGuid;
                        string szServerPath = Server.MapPath(SaveDCConstants.ProfileSnapUploadPath) + szFileUniqueName +
                                              ".jpg";
                        if (File.Exists(szServerPath))
                        {
                            hdnOldProfileImage.Value = szFileUniqueName;
                            hdnProfileImage.Value = szFileUniqueName;
                            hdnProfileImageURL.Value = SaveDCConstants.ProfileSnapUploadPath + szFileUniqueName + ".jpg";
                        }
                    }
                }

                RenderError(Request.QueryString["status"]);
            }
        }

        private void SetCertiInformation(string szCertGuid, string szNum)
        {
            string szServerPath = Server.MapPath(SaveDCConstants.BillsUploadPath) + szCertGuid + ".jpg";
            if (File.Exists(szServerPath))
            {
                if (szNum == "1")
                {
                    hdnOldCerti1.Value = szCertGuid;
                    hdnNewCerti1.Value = szCertGuid;
                }
                else
                {
                    hdnOldCerti2.Value = szCertGuid;
                    hdnNewCerti2.Value = szCertGuid;
                }
            }
        }

        private string UploadCertFileAndReturnName(HttpPostedFile postedFile)
        {
            try
            {
                HttpPostedFile hpf = postedFile;
                if (hpf.ContentLength > 0)
                {
                    if (IsValidImageFile(Path.GetFileName(hpf.FileName)))
                    {
                        string szFileGUID = Guid.NewGuid().ToString();
                        hpf.SaveAs(Server.MapPath(SaveDCConstants.BillsUploadPath) + szFileGUID + ".jpg");
                        return szFileGUID;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return "";
        }

        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            // ajax did not work on the sever so to minimize code changes assigning new file name to hdn control.
            string szFileName = UploadFileAndReturnName();
            if (!string.IsNullOrEmpty(szFileName))
            {
                hdnProfileImage.Value = szFileName;
            }

            string szCert1FileName = UploadCertFileAndReturnName(certificateFile1.PostedFile);
            if (!string.IsNullOrEmpty(szCert1FileName))
            {
                hdnNewCerti1.Value = szCert1FileName;
            }
            string szCert2FileName = UploadCertFileAndReturnName(certificateFile2.PostedFile);
            if (!string.IsNullOrEmpty(szCert2FileName))
            {
                hdnNewCerti2.Value = szCert2FileName;
            }


            // get the edit user id.
            int nEditAppId = 0;
            int.TryParse(hdnEditAppID.Value, out nEditAppId);

            var oApp = new Application
                           {
                               ApplicationId = nEditAppId,
                               StdFirstName = txtStudentFName.Text,
                               StdLastName = txtStudentLName.Text,
                               StdDateOfBirth = Utils.fixNullDate(txtDOB.Text),
                               StdPrevClasses = txtPrevClass.Text,
                               GuardianName = txtGuardianName.Text,
                               GuardianAddress = txtGAddress.Text,
                               ApplicantName = txtApplicantName.Text,
                               ApplicantFatherName = txtApplicantDO.Text,
                               ApplicantContactNum = txtApplicantContactNum.Text,
                               ApplicationNum = txtApplicationNum.Text,
                               ApplicationCategory = rbAppCategory.SelectedValue,
                               ReceivedBy = rbAppReceivedBy.SelectedValue,
                               ReceivedOn = Utils.fixNullDate(txtReceivedOn.Text),
                               ReferredBy = txtReferedBy.Text,
                               ImageGuid = hdnProfileImage.Value,
                               Notes = txtNote.Text,
                               Cert1GUID = hdnNewCerti1.Value,
                               Cert2GUID = hdnNewCerti2.Value,
                               DeliveryNotes = txtDeliveryNotes.Text,
                               FieldVarificationPerson = txtFieldVarificationPerson.Text,
                               BoardNotes = txtBNote.Text
                           };

            int nStatus = new Common().AddNewApplication(oApp);
            if (nStatus > 0)
            {
                // delete prev file.
                try
                {
                    // proflie image
                    if (!string.IsNullOrEmpty(hdnOldProfileImage.Value) &&
                        hdnOldProfileImage.Value != hdnProfileImage.Value)
                    {
                        string szServerPath = Server.MapPath(SaveDCConstants.ProfileSnapUploadPath) +
                                              hdnOldProfileImage.Value + ".jpg";
                        if (File.Exists(szServerPath))
                        {
                            File.Delete(szServerPath);
                        }
                    }


                    // cert 1
                    if (!string.IsNullOrEmpty(hdnOldCerti1.Value) && hdnOldCerti1.Value != hdnNewCerti1.Value)
                    {
                        string szServerPath = Server.MapPath(SaveDCConstants.BillsUploadPath) + hdnOldCerti1.Value +
                                              ".jpg";
                        if (File.Exists(szServerPath))
                        {
                            File.Delete(szServerPath);
                        }
                    }
                    // cert 2
                    if (!string.IsNullOrEmpty(hdnOldCerti2.Value) && hdnOldCerti2.Value != hdnNewCerti2.Value)
                    {
                        string szServerPath = Server.MapPath(SaveDCConstants.BillsUploadPath) + hdnOldCerti2.Value +
                                              ".jpg";
                        if (File.Exists(szServerPath))
                        {
                            File.Delete(szServerPath);
                        }
                    }
                }
                catch
                {
                }

                Response.Redirect("ListApplications.aspx?status=" + (oApp.ApplicationId > 0 ? "5110041" : "5110011"));
            }
            else
            {
                Response.Redirect("AddApplication.aspx?status=5110010");
            }
        }

        private string UploadFileAndReturnName()
        {
            try
            {
                HttpPostedFile hpf = userprofile.PostedFile; //hfc[i];
                if (hpf.ContentLength > 0)
                {
                    if (IsValidImageFile(Path.GetFileName(hpf.FileName)))
                    {
                        string szFileGUID = Guid.NewGuid().ToString();
                        hpf.SaveAs(Server.MapPath(SaveDCConstants.ProfileSnapUploadPath) + szFileGUID + ".jpg");
                        return szFileGUID;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return "";
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

        private bool IsValidImageFile(string szFileName)
        {
            szFileName = szFileName.ToLower();
            if (szFileName.EndsWith(".gif"))
                return true;
            if (szFileName.EndsWith(".jpg"))
                return true;
            if (szFileName.EndsWith(".jpeg"))
                return true;
            if (szFileName.EndsWith(".tiff"))
                return true;
            if (szFileName.EndsWith(".png"))
                return true;
            return false;
        }
    }
}