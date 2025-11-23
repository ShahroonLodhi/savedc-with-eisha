using System;
using System.Data;
using System.Web.UI;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Utils;
using System.Globalization;
using System.Threading;
using OfficeOpenXml;
using System.Text.RegularExpressions;

namespace SaveDC.ControlPanel
{
    public partial class ListApplications : Page
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

                // dont allow operator to edit.
                if (SaveDCSession.UserAccessLevel == UserAccessLevels.Operator
                    )
                {
                    btnDelStudent.Visible = false;
                    trEditStudent.Visible = false;

                    trDisconStd.Visible = false;
                }

                LoadApplications(1);
                RenderError(Request.QueryString["status"]);
            }
            delPopulateData delPopulate = LoadApplications;
            pagerApps.UpdatePageIndex = delPopulate;
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            var oCommon = new Common();
            DataTable dataTable = oCommon.GetAllApplicationsForExcel();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                ExportToExcel(dataTable, "Applications Report");
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

        private void LoadApplications(int nCurrentPage)
        {
            string szSrchStudentName = "";
            if (!string.IsNullOrEmpty(Request.QueryString["StudentName"]))
            {
                szSrchStudentName = Request.QueryString["StudentName"];
                txtStudentName.Text = szSrchStudentName;
            }

            var oCommon = new Common();


            Int32 nTotalRecord = 0;
            DataTable applicationslist = oCommon.GetApplicationsList(szSrchStudentName, nCurrentPage,
                                                                     pagerApps.RecordsPerPage, out nTotalRecord);

            //===============================================================
            pagerApps.TotalRecords = nTotalRecord;
            //===============================================================

            dgStudents.DataSource = applicationslist;
            dgStudents.DataBind();


            // hide/show grid if no rec found
            if (nTotalRecord <= 0)
            {
                tbDataFound.Visible = false;
                tbNoDataFound.Visible = true;
                // set the total
                lblTotal.Text = "0";
            }
            else
            {
                tbDataFound.Visible = true;
                tbNoDataFound.Visible = false;
                // set the total
                lblTotal.Text = nTotalRecord.ToString();
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
            lblError.CssClass = !szErrorCode.EndsWith("1") ? "FailureMessage" : "SuccessMessage";

            lblError.Text = szErrorDesc;
        }

        protected void searchbtn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ListApplications.aspx?StudentName=" + txtStudentName.Text);
        }

        protected void btnEditStudent_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            Response.Redirect("AddApplication.aspx?AppId=" + hdnAppID.Value);
        }

        protected void btnDelStudent_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            int szCurStudentId = Utils.fixNullInt(hdnAppID.Value);

            string szStatus = "5110020";
            var oCommon = new Common();
            int nStatus = oCommon.DeleteApplication(szCurStudentId);
            if (nStatus > 0)
            {
                szStatus = "5110021";
            }

            Response.Redirect("ListApplications.aspx?status=" + szStatus);
        }

        protected void btnDetailStudent_Click(object sender, EventArgs e)
        {
            Response.Redirect("DetailsApplication.aspx?AppId=" + hdnAppID.Value);
        }

        protected void btnMoveApplication_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            var oCommon = new Common();
            oCommon.MoveApplicationToStudent(Utils.fixNullInt(hdnAppID.Value));
            Response.Redirect("ListApplications.aspx?status=5110031");
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