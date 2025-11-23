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
using System.Data;
using System.Text.RegularExpressions;
using System.Linq;

namespace SaveDC.ControlPanel
{
    public partial class ListSchools : Page
    {
        #region Delegates

        public delegate void delPopulateData(int nCurrentPage);

        #endregion

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
                //Based on new Feedback operator can now edit, send sms & send history 1/22/2022
                if (SaveDCSession.UserAccessLevel == UserAccessLevels.Operator)
                {
                    btnDelSchool.Visible = false;
                    //btnEditSchool.Visible = false;
                    //btnSendSms.Visible = false;
                    btnSmsHistory.Visible = false;
                }


                LoadSchools(1);
                RenderError(Request.QueryString["status"]);
            }
            delPopulateData delPopulate = LoadSchools;
            pagerApps.UpdatePageIndex = delPopulate;
        }


        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            var school = new School();
            var oSchoolManager = new SchoolManager(school);
            DataSet ds = oSchoolManager.GetSchoolsInDS();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    ExportToExcel(ds.Tables[0], "Schools Report");
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
                    dt2.Columns.Remove("SchoolId");
                    dt2.Columns.Remove("Address");
                    dt2.Columns.Remove("Notes");
                    dt2.Columns.Remove("isDeleted");
                    dt2.Columns.Remove("IID");
                    dt2.Columns.Remove("TableRecID");
                    dt2.Columns["SchoolName"].ColumnName = "School Name";
                    dt2.Columns["PrincipalName"].ColumnName = "Principal Name";
                    dt2.Columns["SocialOrganizerName"].ColumnName = "S.Organizer";
                    dt2.Columns["PhoneNum"].ColumnName = "Phone No.";
                    dt2.Columns["Email"].ColumnName = "Email Address";
                    dt2.Columns["TotalStudents"].ColumnName = "Total Ss";
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

        private void LoadSchools(int nCurrentPage)
        {
            string szSrchSchoolName = "";
            if (!string.IsNullOrEmpty(Request.QueryString["SchoolName"]))
            {
                szSrchSchoolName = Request.QueryString["SchoolName"];
                txtSchoolName.Text = szSrchSchoolName;
            }

            var school = new School();
            school.SchoolName = szSrchSchoolName;

            var oSchoolManager = new SchoolManager(school);
            School[] schools = oSchoolManager.GetSchools(nCurrentPage, pagerApps.RecordsPerPage);
            Session["SchoolList"] = schools;
            Session["filteredSchools"] = schools.Where(c => c.TotalStudents == 0).ToArray();
            //===============================================================
            pagerApps.TotalRecords = Convert.ToInt32(oSchoolManager.RecordCount);
            //===============================================================

            dgSchools.DataSource = schools;
            dgSchools.DataBind();


            // hide/show grid if no rec found
            if (schools == null || schools.Length <= 0)
            {
                // set the total
                lblTotal.Text = 0.ToString();
                tbDataFound.Visible = false;
                tbNoDataFound.Visible = true;
            }
            else
            {
                // set the total
                lblTotal.Text = schools.Length.ToString();
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
            string szSrchSchoolName = txtSchoolName.Text;
            //if (!string.IsNullOrEmpty(Request.Form["ctl00$ContentPlaceHolder1$txtSchoolName"]))
            //    szSrchSchoolName = Request.Form["ctl00$ContentPlaceHolder1$txtSchoolName"];

            Response.Redirect("ListSchools.aspx?SchoolName=" + szSrchSchoolName);
        }

        protected void dgSchools_Databound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string curSchoolStdCount =
                    Utils.fixNullString(((HiddenField) e.Item.FindControl("hdnSchoolStdCount")).Value);
                if (curSchoolStdCount == "0")
                {
                    e.Item.Style.Add("background-color", "red");
                    e.Item.Style.Add("color", "white");
                }
            }
        }

        protected void btnEditSchool_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szCurSchoolId = hdnSchoolID.Value;
            Response.Redirect("AddSchool.aspx?SchoolId=" + szCurSchoolId);
        }


        protected void btnDelSchool_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szCurSchoolId = hdnSchoolID.Value;

            var oSchool = new School();
            oSchool.SchoolID = int.Parse(szCurSchoolId);

            string szStatus = "5021060";
            var oSchoolManager = new SchoolManager(oSchool);
            if (oSchoolManager.Delete())
            {
                szStatus = "5021061";
            }

            Response.Redirect("ListSchools.aspx?status=" + szStatus);
        }

        protected void btnDetails_Click(object sender, EventArgs e)
        {
            string szCurSchoolId = hdnSchoolID.Value;
            Response.Redirect("DetailsSchool.aspx?SchoolId=" + szCurSchoolId);
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szCurUserId = hdnSchoolID.Value;
            Response.Redirect("SendEmailToSchool.aspx?SchoolId=" + szCurUserId);
        }

        protected void btnSendSMS_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szCurSchoolId = hdnSchoolID.Value;
            Response.Redirect("SendSMSToSchool.aspx?SchoolId=" + szCurSchoolId);
        }

        protected void btnSMSHistory_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szCurSchoolId = hdnSchoolID.Value;
            Response.Redirect("ListSchoolSMS.aspx?SchoolId=" + szCurSchoolId + "&Type=SCH");
        }

        protected string ConvertToTitleCase(string textToConvert)
        {
            string txt = textToConvert.Trim();
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo ti = cultureInfo.TextInfo;
            txt = ti.ToTitleCase(txt);
            return txt;
        }

        protected void btnShowHideList_Click(object sender, EventArgs e)
        {
            School[] schools = Session["SchoolList"] as School[];
            School[] filteredSchools = Session["filteredSchools"] as School[];


            if (btnShowHideList.Text.Equals("[ Show Discontinue List ]")) //Enable Discontinue List
            {
                btnShowHideList.Text = "[ Hide Discontinue List ]";
                if(filteredSchools.Length > 0)
                {
                    dgSchools.DataSource = filteredSchools;
                    dgSchools.DataBind();

                    // hide/show grid if no rec found
                    if (filteredSchools == null || filteredSchools.Length <= 0)
                    {
                        // set the total
                        lblTotal.Text = 0.ToString();
                        tbDataFound.Visible = false;
                        tbNoDataFound.Visible = true;
                    }
                    else
                    {
                        // set the total
                        lblTotal.Text = filteredSchools.Length.ToString();
                        tbDataFound.Visible = true;
                        tbNoDataFound.Visible = false;
                    }



                }
            }
            else if (btnShowHideList.Text.Equals("[ Hide Discontinue List ]")) //Disable Discontinue List
            {
                btnShowHideList.Text = "[ Show Discontinue List ]";
                dgSchools.DataSource = schools;
                dgSchools.DataBind();

                // hide/show grid if no rec found
                if (schools == null || schools.Length <= 0)
                {
                    // set the total
                    lblTotal.Text = 0.ToString();
                    tbDataFound.Visible = false;
                    tbNoDataFound.Visible = true;
                }
                else
                {
                    // set the total
                    lblTotal.Text = schools.Length.ToString();
                    tbDataFound.Visible = true;
                    tbNoDataFound.Visible = false;
                }
            }
        }
    }
}