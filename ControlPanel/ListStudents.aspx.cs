using System;
using System.IO;
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
    public partial class ListStudents : Page
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
                                                          UserAccessLevels.Operator, UserAccessLevels.Donor
                                                      });
                oValidator = null;

                // dont allow operator to edit.
                if (SaveDCSession.UserAccessLevel == UserAccessLevels.Operator ||
                    SaveDCSession.UserAccessLevel == UserAccessLevels.Donor)
                {
                    btnDelStudent.Visible = false;
                    trEditStudent.Visible = false;
                    trAssignDonor.Visible = false;
                    trUnAssignDonor.Visible = false;
                    trDisconStd.Visible = false;
                    if (SaveDCSession.UserAccessLevel == UserAccessLevels.Donor)
                    {
                        trAddStudent.Visible = false;
                        trExpense.Visible = false;
                        trVarify.Visible = false;
                        trStatusSrch.Visible = false;
                    }
                }

                // allow few operators to edit.
                if (SaveDCSession.UserName.ToLower() == "shamsa" || SaveDCSession.UserName.ToLower() == "muzaffar" || SaveDCSession.UserName.ToLower() == "irfan")
                {
                    trEditStudent.Visible = true;
                }
                //if (SaveDCSession.UserAccessLevel != UserAccessLevels.Operator)
                //{
                //    btnVarify.Visible = false;
                //}
                txtClassLevel.Items.Add(new ListItem("All Students", "0"));
                txtClassLevel.Items.Add(new ListItem("Junior Students", "1"));
                txtClassLevel.Items.Add(new ListItem("Senior Students", "2"));

                txtClassLevel.SelectedIndex = (Session["selectedIndex"] == null)? 0 : Convert.ToInt32(Session["selectedIndex"]);

                LoadStudents(1);
                RenderError(Request.QueryString["status"]);
            }
            else
            {
                Session["selectedIndex"] = txtClassLevel.SelectedIndex;

                txtClassLevel.Items.Add(new ListItem("All Students", "0"));
                txtClassLevel.Items.Add(new ListItem("Junior Students", "1"));
                txtClassLevel.Items.Add(new ListItem("Senior Students", "2"));

                txtClassLevel.SelectedIndex = Convert.ToInt32(Session["selectedIndex"]);
            }

            delPopulateData delPopulate = LoadStudents;
            pagerApps.UpdatePageIndex = delPopulate;
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            var oStudent = new Student();
            oStudent.DonorId = 0;
            oStudent.StatusId = 3;
            int nVarified = 0;
            oStudent.IsVarificationExists = nVarified == 1; ;
            var oStudentManager = new StudentManager(oStudent);
            DataSet ds = oStudentManager.GetStudentsInDS(0);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    ExportToExcel(ds.Tables[0], "Students Report");
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
                    dt2.Columns.Remove("StudentId");
                    dt2.Columns.Remove("DOB");
                    dt2.Columns.Remove("ImageGUID");
                    dt2.Columns.Remove("Note");
                    dt2.Columns.Remove("EducationalLevel");
                    dt2.Columns.Remove("StatusId");
                    dt2.Columns.Remove("FamilyId");
                    dt2.Columns.Remove("HistoryId");
                    dt2.Columns.Remove("DonorId");
                    dt2.Columns.Remove("ClassId");
                    dt2.Columns.Remove("SchoolId");
                    dt2.Columns.Remove("IsDeleted");
                    dt2.Columns.Remove("IsVarified");
                    dt2.Columns["SchoolName"].ColumnName = "School Name";
                    dt2.Columns["ClassName"].ColumnName = "Class Name";
                    dt2.Columns["FamilyName"].ColumnName = "Family Name";
                    dt2.Columns["StatusDesc"].ColumnName = "Status";
                    dt2.Columns["DonorName"].ColumnName = "Donor Name";
                    dt2.Columns["Family Name"].SetOrdinal(3);


                    //dt2.Columns.Remove("IID");
                    //dt2.Columns.Remove("TableRecID");
                    //dt2.Columns["SchoolName"].ColumnName = "School Name";
                    //dt2.Columns["PrincipalName"].ColumnName = "Principal Name";
                    //dt2.Columns["SocialOrganizerName"].ColumnName = "S.Organizer";
                    //dt2.Columns["PhoneNum"].ColumnName = "Phone No.";
                    //dt2.Columns["Email"].ColumnName = "Email Address";
                    //dt2.Columns["TotalStudents"].ColumnName = "Total Ss";
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

        private void LoadStudents(int nCurrentPage)
        {
            string szSrchStudentName = "";
            if (!string.IsNullOrEmpty(Request.QueryString["StudentName"]))
            {
                szSrchStudentName = Request.QueryString["StudentName"];
                txtStudentName.Text = szSrchStudentName;
            }
            int nStdStatusID = 3;
            int nVarified = 0;

            if (!string.IsNullOrEmpty(Request.QueryString["Sts"]))
            {
                string selectedVal = Request.QueryString["Sts"];

                if (selectedVal.Contains("-"))
                {
                    string[] splited = selectedVal.Split((new[] {'-'}));
                    nStdStatusID = Utils.fixNullInt(splited[0]);
                    nVarified = Utils.fixNullInt(splited[1]);
                }
                else
                {
                    nStdStatusID = Utils.fixNullInt(selectedVal);
                }
                rblStatus.SelectedValue = Request.QueryString["Sts"];
            }

            int nDonorId = 0;
            if (SaveDCSession.UserAccessLevel == UserAccessLevels.Donor)
            {
                nDonorId = SaveDCSession.UserId;
                nStdStatusID = 0;
            }

            var oStudent = new Student();
            oStudent.FirstName = szSrchStudentName;
            oStudent.DonorId = nDonorId;
            oStudent.StatusId = nStdStatusID;
            oStudent.IsVarificationExists = nVarified == 1;
            txtSchoolName.Text = oStudent.SchoolName = Request.QueryString["sn"] ?? "";
            txtFamilyName.Text = oStudent.FamilyName = Request.QueryString["fn"] ?? "";


            var oStudentManager = new StudentManager(oStudent);
            Student[] students = oStudentManager.GetStudents((Session["selectedIndex"] == null) ? 0 : Convert.ToInt32(Session["selectedIndex"]), nCurrentPage, pagerApps.RecordsPerPage);


            //===============================================================
            pagerApps.TotalRecords = Convert.ToInt32(oStudentManager.RecordCount);
            //===============================================================

            dgStudents.DataSource = students;
            dgStudents.DataBind();


            // hide/show grid if no rec found
            if (students == null || students.Length <= 0)
            {
                tbDataFound.Visible = false;
                tbNoDataFound.Visible = true;
                // set the total
                lblTotal.Text = 0.ToString();
            }
            else
            {
                tbDataFound.Visible = true;
                tbNoDataFound.Visible = false;
                // set the total
                lblTotal.Text = students.Length.ToString();

                if (SaveDCSession.UserAccessLevel == UserAccessLevels.Donor && dgStudents.Columns.Count > 6)
                {
                    //dgStudents.Columns.RemoveAt(3);
                    dgStudents.Columns.RemoveAt(5);
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

        protected void searchbtn_Click(object sender, ImageClickEventArgs e)
        {
            string szSrchStudentName = txtStudentName.Text;
            //if (!string.IsNullOrEmpty(Request.Form["ctl00$ContentPlaceHolder1$txtStudentName"]))
            //    szSrchStudentName = Request.Form["ctl00$ContentPlaceHolder1$txtStudentName"];

            Response.Redirect("ListStudents.aspx?StudentName=" + szSrchStudentName + "&Sts=" +
                              rblStatus.SelectedValue + "&fn=" + txtFamilyName.Text + "&sn=" +
                              txtSchoolName.Text);
        }

        protected void dgStudents_Databound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var oCommon = new Common();


                bool bIsVarificationExists =
                    Utils.fixNullBool(((HiddenField) e.Item.FindControl("IsVarificationExists")).Value);

                int nStatusId = Utils.fixNullInt(((HiddenField) e.Item.FindControl("StatusId")).Value);
                {
                    string StatusName = Utils.fixNullString(((HiddenField) e.Item.FindControl("StatusName")).Value);
                    if (nStatusId == 1)
                    {
                        if (!bIsVarificationExists)
                        {
                            StatusName = "<font color = 'Violet'>" + "In Process; Verification Required" + "</font>";
                        }
                        else
                        {
                            StatusName = "<font color = '#FF9933'>" + "Verified but Approval Required" + "</font>";
                        }
                    }
                    if (nStatusId == 2)
                    {
                        StatusName = "<font color = 'Navy'>" + StatusName + "</font>";
                    }
                    if (nStatusId == 3)
                    {
                        StatusName = "<font color = 'Green'>" + StatusName + "</font>";
                    }
                    if (nStatusId == 4)
                    {
                        StatusName = "<font color = 'DarkRed'>" + StatusName + "</font>";
                    }
                    if (nStatusId == 5)
                    {
                        StatusName = "<font color = 'DarkGreen'>" + StatusName + "</font>";
                    }
                    if (nStatusId == 6)
                    {
                        StatusName = "<font color = 'red'>" + StatusName + "</font>";
                    }

                    int nColIndex = 6;
                    if (SaveDCSession.UserAccessLevel == UserAccessLevels.Donor)
                        nColIndex -= 1;
                    e.Item.Cells[nColIndex].Text = StatusName;
                }
            }
        }

        protected void btnEditStudent_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szCurStudentId = hdnStudentID.Value;
            Response.Redirect("AddStudent.aspx?StudentId=" + szCurStudentId);
        }

        protected void btnExpense_Click(object sender, EventArgs e)
        {
            string szCurStudentId = hdnStudentID.Value;
            SaveDCSession.StudentId = int.Parse(szCurStudentId);
            Response.Redirect("ListStudentEstimations.aspx");
        }

        protected void btnDelStudent_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szCurStudentId = hdnStudentID.Value;

            var oStudent = new Student();
            oStudent.StudentId = int.Parse(szCurStudentId);

            string szStatus = "5031100";
            var oStudentManager = new StudentManager(oStudent);
            oStudent = oStudentManager.Load();

            if (oStudentManager.Delete())
            {
                szStatus = "5031101";

                // delete profile picture.
                try
                {
                    // bill1
                    string szServerPath = Server.MapPath(SaveDCConstants.ProfileSnapUploadPath) + oStudent.ImageGUID +
                                          ".jpg";
                    if (File.Exists(szServerPath))
                    {
                        File.Delete(szServerPath);
                    }
                }
                catch
                {
                }
            }

            Response.Redirect("ListStudents.aspx?status=" + szStatus);
        }

        protected void btnProgressReport_Click(object sender, EventArgs e)
        {
            string szCurStudentId = hdnStudentID.Value;
            SaveDCSession.StudentId = int.Parse(szCurStudentId);
            Response.Redirect("ListProgressReports.aspx");
        }

        protected void btnAnnualProgressReport_Click(object sender, EventArgs e)
        {
            string szCurStudentId = hdnStudentID.Value;
            SaveDCSession.StudentId = int.Parse(szCurStudentId);
            Response.Redirect("ListAnnualProgressReports.aspx");
        }

        protected void btnAttachments_Click(object sender, EventArgs e)
        {
            string szCurStudentId = hdnStudentID.Value;
            SaveDCSession.StudentId = int.Parse(szCurStudentId);
            Response.Redirect("ListAttachments.aspx");

            /*string szCurStudentId = hdnStudentID.Value;
            SaveDCSession.StudentId = int.Parse(szCurStudentId);

            int nStudentId = SaveDCSession.StudentId;

            var oCommon = new Common();

            var oStudent = new Student();
            oStudent.StudentId = nStudentId;
            var oStudentManager = new StudentManager(oStudent);
            oStudent = oStudentManager.Load();

            Int32 nTotalRecord = 0;
            DataSet oSqlDataSet = oCommon.LoadStudentLeavingCertificate(nStudentId, out nTotalRecord);

            if (SaveDCSession.UserAccessLevel == UserAccessLevels.Donor)
            {
                if (nTotalRecord != 0)
                {

                    int nReportId = Utils.fixNullInt(oSqlDataSet.Tables[0].Rows[0].ItemArray[2].ToString());
                    Response.Redirect("DetailsLeavingCertificate.aspx?ReportId=" + nReportId.ToString());
                }
                else
                    Response.Redirect("ListStudents.aspx?status=5031280");
            }
            else
            {
                if (nTotalRecord == 0)
                    Response.Redirect("AddLeavingCertificate.aspx");
                else
                {

                    int nReportId = Utils.fixNullInt(oSqlDataSet.Tables[0].Rows[0].ItemArray[2].ToString());
                    Response.Redirect("AddLeavingCertificate.aspx?ReportId=" + nReportId.ToString());
                }
            }*/
        }

        protected void btnAssignDonor_Click(object sender, EventArgs e)
        {
            string szCurStudentId = hdnStudentID.Value;
            // SESSION.StudentId = int.Parse(szCurStudentId);
            Response.Redirect("AssignDonor.aspx?StudentId=" + szCurStudentId);
        }

        protected void btnDetailStudent_Click(object sender, EventArgs e)
        {
            string szCurStudentId = hdnStudentID.Value;
            // SESSION.StudentId = int.Parse(szCurStudentId);
            Response.Redirect("DetailsStudent.aspx?StudentId=" + szCurStudentId);
        }

        protected void btnVarify_Click(object sender, EventArgs e)
        {
            string szCurStudentId = hdnStudentID.Value;
            // SESSION.StudentId = int.Parse(szCurStudentId);
            Response.Redirect("VarifyStudent.aspx?StudentId=" + szCurStudentId);
        }

        protected void btnUnAssignDonor_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szStatus = "5031250";
            string szCurStudentId = hdnStudentID.Value;

            var oCommon = new Common();
            int nStatus = oCommon.AssignDonor(szCurStudentId, "0");
            if (nStatus > 0)
            {
                szStatus = "5031251";
                Response.Redirect("ListStudents.aspx?status=" + szStatus);
            }
            else
            {
                szStatus = "5031251";
                Response.Redirect("ListStudents.aspx?status=" + szStatus);
            }
        }

        protected void btnDisconStudent_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szCurStudentId = hdnStudentID.Value;

            var oCommon = new Common();
            oCommon.DiscontinueStudent(szCurStudentId);
            //szStatus = "5031261";
            Response.Redirect("ListStudents.aspx?status=5031261");
            //}
            //else
            //{
            //    Response.Redirect("ListStudents.aspx?status=" + szStatus);
            //}
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