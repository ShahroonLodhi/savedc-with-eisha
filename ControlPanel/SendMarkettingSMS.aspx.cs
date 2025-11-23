using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class SendMarkettingSMS : Page
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

                if(SaveDCSession.UserAccessLevel == UserAccessLevels.Operator)
                {
                    MaskedEditExtender2.Enabled = true;
                }
            }
        }

        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string errorCode = "";
            string recipients = "";

            //Removing the MaskedEditExtender default Mask
            if (SaveDCSession.UserAccessLevel == UserAccessLevels.Operator
                && txtPhoneNum.Text.Equals("+92__________"))
            {
                txtPhoneNum.Text = String.Empty;
            }

            if (txtPhoneNum.Text == "")
            {
                // upload file here.
                if (Request.Files.Count == 0)
                {
                    Response.Redirect("ListMarkettingSMS.aspx?status=5091006");
                    return;
                }
                string strmContents = ReadFile();

                recipients = strmContents;
                if (string.IsNullOrEmpty(recipients))
                {
                    Response.Redirect("ListMarkettingSMS.aspx?status=5091006");
                    return;
                }
            }
            else
                recipients = txtPhoneNum.Text;

            //Before Sending the SMS, here we will check the reciepientsList if the user is Operator, as operator can only send local sms.
            if(SaveDCSession.UserAccessLevel == UserAccessLevels.Operator)
            {
                try
                {
                    var recipientsList = new List<SMSRecipient>();

                    List<string> recipientNum = recipients.Split(new[] { '\n' }).ToList();
                    foreach (string recipi in recipientNum)
                    {
                        if (string.IsNullOrEmpty(recipi.Trim())) continue;

                        if (recipi.Contains(","))
                        {
                            string[] splitedRecpi = recipi.Split(new[] { ',' });
                            recipientsList.Add(new SMSRecipient
                            { Name = splitedRecpi[0], PhoneNum = splitedRecpi[1].Replace("\r", "") });
                        }
                        else
                        {
                            recipientsList.Add(new SMSRecipient { Name = "", PhoneNum = recipi });
                        }
                    }
                    if(recipientsList.Count > 0)
                    {
                        foreach(var li in recipientsList)
                        {
                            if (!li.PhoneNum.StartsWith("+92"))
                            {
                                Response.Redirect("ListMarkettingSMS.aspx?status=9999999");
                                return;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Utils.LogErrorToFile(ex);
                    return;
                }
            }

            var commom = new Common();
            int statusCode = commom.SendSMSAndLogInDatabase(0, recipients, txtMessage.Text, "MAR");

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

            Response.Redirect("ListMarkettingSMS.aspx?status=" + errorCode);
        }

        private string ReadFile()
        {
            HttpPostedFile postedFile = Request.Files[0];
            int strLen = Convert.ToInt32(postedFile.ContentLength);
            // Create a byte array.
            var strArr = new byte[strLen];
            // Read stream into byte array.
            postedFile.InputStream.Read(strArr, 0, strLen);

            return Encoding.ASCII.GetString(strArr);
        }
    }
}