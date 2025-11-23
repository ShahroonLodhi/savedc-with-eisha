using System;
using System.Data;
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

namespace SaveDC.ControlPanel
{
    public partial class ListEmployee : Page
    {
        #region Delegates

        public delegate void delPopulateData(int nCurrentPage);

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            // page validation
            var oValidator = new Validator();
            oValidator.ValidateRequest(Request);
            oValidator.ValidateUserPageAccess(SaveDCSession.UserAccessLevel,
                                              new[] {UserAccessLevels.SuperAdmin, UserAccessLevels.Admin});
            oValidator = null;

            if (!Page.IsPostBack)
            {
                LoadUsers(1);
                RenderError(Request.QueryString["status"]);
            }
            delPopulateData delPopulate = LoadUsers;
            pagerApps.UpdatePageIndex = delPopulate;
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
            var oCommon = new Common();
            oUser.UserName = szSrchUserName;
            DataTable dtEmployee = new DataTable();

            dtEmployee.Load(oCommon.GetEmployees());
            
            if(dtEmployee!=null && dtEmployee.Rows.Count > 0)
            {
                //===============================================================
                pagerApps.TotalRecords = Convert.ToInt32(dtEmployee.Rows.Count);
                //===============================================================


                dgUsers.DataSource = dtEmployee;
                dgUsers.DataBind();

                lblTotal.Text = Convert.ToString(dtEmployee.Rows.Count);
                tbDataFound.Visible = true;
                tbNoDataFound.Visible = false;
            }
            else
            {
                // set the total
                lblTotal.Text = 0.ToString();
                tbNoDataFound.Visible = true;
                tbDataFound.Visible = false;
            }
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            var oCommon = new Common();
            DataTable dataTable = new DataTable();
            dataTable.Load(oCommon.GetEmployees());
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                ExportToExcel(dataTable, "Employee Report");
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
                    dt2.Columns.Remove("EmployeeId");
                    dt2.Columns.Remove("FirstName");
                    dt2.Columns.Remove("LastName");
                    dt2.Columns.Remove("UserCategoryId");
                    dt2.Columns.Remove("Salary");
                    dt2.Columns.Remove("isActive");
                    dt2.Columns.Remove("Gender");
                    dt2.Columns.Remove("Notes");
                    dt2.Columns["EmployeeName"].ColumnName = "Employee Name";
                    dt2.Columns["PhoneNumber"].ColumnName = "Phone Number";
                    dt2.Columns["CNIC"].SetOrdinal(1);
                    dt2.Columns["DOB"].SetOrdinal(2);
                    dt2.Columns["Phone Number"].SetOrdinal(3);
                    dt2.Columns["Email"].SetOrdinal(4);
                    dt2.Columns["Address"].SetOrdinal(dt2.Columns.Count - 1);
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

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szCurUserId = hdnUserID.Value;
            Response.Redirect("SendEmailToUser.aspx?UserId=" + szCurUserId);
        }

        protected void searchbtn_Click(object sender, ImageClickEventArgs e)
        {
            string szSrchUserName = "";
            if (!string.IsNullOrEmpty(txtUserName.Text))
                szSrchUserName = txtUserName.Text;

            Response.Redirect("ListEmployee.aspx?UserName=" + szSrchUserName);
        }

        protected void btnSendSMS_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szCurUserId = hdnUserID.Value;
            Response.Redirect("SendSMSToUser.aspx?UserId=" + szCurUserId);
        }

        protected void btnSMSHistory_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szCurUserId = hdnUserID.Value;
            Response.Redirect("ListUserSMS.aspx?UserId=" + szCurUserId);
        }

        protected void btnEmailsHistory_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szCurUserId = hdnUserID.Value;
            Response.Redirect("ListUserEmails.aspx?UserId=" + szCurUserId);
        }

        protected void dgUsers_Databound(object sender, DataGridItemEventArgs e)
        {
        }

        protected void btnDetail_Click(object sender, EventArgs e)
        {
            string szCurUserId = hdnUserID.Value;
            Response.Redirect("DetailEmployee.aspx?EmployeeId=" + szCurUserId);
        }

        protected void btnDelUser_Click(object sender, EventArgs e)
        {
            string szCurUserId = hdnUserID.Value;

            var oEmployee = new Employee();
            oEmployee.EmployeeID = int.Parse(szCurUserId);

            string szStatus = "5011270";
            var oEmployeeManager = new EmployeeManager(oEmployee);
            if (oEmployeeManager.Delete())
            {
                szStatus = "5011271";
            }

            Response.Redirect("ListEmployee.aspx?status=" + szStatus);
        }

        protected void btnEditUser_Click(object sender, EventArgs e)
        {
            string szCurUserId = hdnUserID.Value;
            Response.Redirect("AddEmployee.aspx?EmployeeId=" + szCurUserId);
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