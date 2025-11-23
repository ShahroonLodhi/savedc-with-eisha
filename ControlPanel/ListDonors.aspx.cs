using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;
using System.Globalization;
using System.Threading;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Data;

namespace SaveDC.ControlPanel
{
    public partial class ListDonors : Page
    {
        #region Delegates

        public delegate void delPopulateData(int nCurrentPage);

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            // page validation
            var oValidator = new Validator();
            oValidator.ValidateRequest(Request);
            oValidator.ValidateUserPageAccess(SaveDCSession.UserAccessLevel, new[] {UserAccessLevels.SuperAdmin});

            if (!Page.IsPostBack)
            {
                LoadUsers(1);
                RenderError(Request.QueryString["status"]);
            }
            delPopulateData delPopulate = LoadUsers;
            pagerApps.UpdatePageIndex = delPopulate;
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            var oUser = new User();
            oUser.UserRoleID = 4;
            var oUserManager = new UserManager(oUser);
            DataSet ds = oUserManager.GetUsersInDS();
            if(ds!=null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    ExportToExcel(ds.Tables[0], "Donor Report");
                }
            }
            
        }

        private void ExportToExcel(DataTable dt, string fileName)
        {
            #region Export Datatable to excel
            try
            {
                if (dt.Rows.Count >= 1)
                {
                    DataTable dt2 = dt.Copy();
                    dt2.Columns.Remove("UserId");
                    dt2.Columns.Remove("password");
                    dt2.Columns.Remove("Note");
                    dt2.Columns.Remove("UserRoleID");
                    dt2.Columns.Remove("isDeleted");
                    dt2.Columns.Remove("isSuspended");
                    dt2.Columns.Remove("Address");
                    dt2.Columns.Remove("Country");
                    dt2.Columns.Remove("Gender");
                    dt2.Columns.Remove("CNIC");
                    dt2.Columns.Remove("Occupation");
                    dt2.Columns.Remove("Qualification");
                    dt2.Columns.Remove("RecevingDate");
                    dt2.Columns.Remove("RoleName");
                    dt2.Columns["UserName"].ColumnName = "Donor Name";
                    dt2.Columns["FirstName"].ColumnName = "First Name";
                    dt2.Columns["LastName"].ColumnName = "Last Name";
                    dt2.Columns["PhoneNum"].ColumnName = "Phone No.";
                    dt2.Columns["Email"].ColumnName = "Email Address";
                    dt2.Columns["LastSMSDate"].ColumnName = "Last SMS Sent On";
                    dt2.Columns["Phone No."].SetOrdinal(3);
                    dt2.Columns["Email Address"].SetOrdinal(4);
                    //dt2.Columns["Phone Number"].SetOrdinal(3);
                    //dt2.Columns["Email"].SetOrdinal(4);
                    //dt2.Columns["Address"].SetOrdinal(dt2.Columns.Count - 1);
                    ExcelPackage pck = new ExcelPackage();
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add(fileName);
                    RemoveHtmlTags(dt2);
                    ws.Cells["A1"].LoadFromDataTable(dt2, true);
                    FindColumnDataType(ws, dt2);
                    ws.Cells["A:Z"].AutoFitColumns();
                    ws.Cells["A1:Z1"].Style.Font.Bold = true;

                    //}
                    //}
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + ".xlsx");
                    pck.SaveAs(Response.OutputStream);
                    Response.End();

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alert", "alert('No Data found for Export to Excel.');", true);

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            #endregion



        }

        public void RemoveHtmlTags(DataTable dt)
        {
            string pattern = @"<(.|\n)*?>";
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[j][i].ToString()) && dt.Columns[i].DataType != typeof(System.DateTime))
                    {
                        string sOut = Regex.Replace(dt.Rows[j][i].ToString(), pattern, string.Empty);
                        sOut = sOut.Replace("&nbsp;", String.Empty);
                        sOut = sOut.Replace("&amp;", String.Empty);
                        sOut = sOut.Replace("&gt;", String.Empty);
                        sOut = sOut.Replace("&lt;", String.Empty);
                        dt.Rows[j][i] = sOut;
                    }
                }
            }
        }

        public void FindColumnDataType(ExcelWorksheet ws, DataTable dt)
        {


            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].DataType == typeof(System.DateTime))
                {
                    ws.Column(i + 1).Style.Numberformat.Format = "MM/DD/YYYY";

                }
            }


        }

        private void LoadUsers(int currentPageIndex)
        {
            string szSrchUserName = "";
            if (!string.IsNullOrEmpty(Request.QueryString["UserName"]))
            {
                szSrchUserName = Request.QueryString["UserName"];
                txtUserName.Text = szSrchUserName;
            }

            var oUser = new User();
            var oUserManager = new UserManager(oUser);
            oUser.UserName = szSrchUserName;
            oUser.UserRoleID = 4;
            User[] users = oUserManager.GetUsers(currentPageIndex, pagerApps.RecordsPerPage);

            //===============================================================
            pagerApps.TotalRecords = Convert.ToInt32(oUserManager.RecordCount);
            //===============================================================

            dgUsers.DataSource = users;
            dgUsers.DataBind();

            if (users == null || users.Length <= 0)
            {
                // set the total
                lblTotal.Text = 0.ToString();
                tbNoDataFound.Visible = true;
                tbDataFound.Visible = false;
            }
            else
            {
                lblTotal.Text = users.Length.ToString();
                tbDataFound.Visible = true;
                tbNoDataFound.Visible = false;
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

        protected void searchbtn_Click(object sender, ImageClickEventArgs e)
        {
            string szSrchUserName = "";
            if (!string.IsNullOrEmpty(txtUserName.Text))
                szSrchUserName = txtUserName.Text;

            Response.Redirect("ListDonors.aspx?UserName=" + szSrchUserName);
        }

        protected void dgUsers_Databound(object sender, DataGridItemEventArgs e)
        {
        }

        protected void btnDetail_Click(object sender, EventArgs e)
        {
            string szCurUserId = hdnUserID.Value;
            Response.Redirect("DetailUsers.aspx?UserId=" + szCurUserId + "&Role=donor");
        }

        protected void btnDelUser_Click(object sender, EventArgs e)
        {
            string szCurUserId = hdnUserID.Value;

            var oUser = new User();
            oUser.UserID = int.Parse(szCurUserId);

            string szStatus = "5011270";
            var oUserManager = new UserManager(oUser);
            if (oUserManager.Delete())
            {
                szStatus = "5011271";
            }

            Response.Redirect("ListDonors.aspx?status=" + szStatus);
        }

        protected void btnEditUser_Click(object sender, EventArgs e)
        {
            string szCurUserId = hdnUserID.Value;
            Response.Redirect("AddUsers.aspx?UserId=" + szCurUserId + "&Role=donor");
        }

        protected void btnSendSMS_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szCurUserId = hdnUserID.Value;
            Response.Redirect("SendSMSToDonor.aspx?DonorId=" + szCurUserId);
        }

        protected void btnAddRemarks_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szCurUserId = hdnUserID.Value;
            Response.Redirect("AddRemarksForDonor.aspx?DonorId=" + szCurUserId);
        }

        protected void btnAddNotes_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szCurUserId = hdnUserID.Value;
            Response.Redirect("AddNotesForDonor.aspx?DonorId=" + szCurUserId);
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szCurUserId = hdnUserID.Value;
            Response.Redirect("SendEmailToDonor.aspx?DonorId=" + szCurUserId);
        }

        protected void btnSMSHistory_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szCurUserId = hdnUserID.Value;
            Response.Redirect("ListDonorSMS.aspx?DonorId=" + szCurUserId);
        }

        protected void btnEmailHistory_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szCurUserId = hdnUserID.Value;
            Response.Redirect("ListDonorEmails.aspx?DonorId=" + szCurUserId);
        }

        protected string ConvertToTitleCase(string textToConvert)
        {
            string txt = textToConvert.Trim();
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo ti = cultureInfo.TextInfo;
            txt = ti.ToTitleCase(txt);
            return txt;
        }
    }
}