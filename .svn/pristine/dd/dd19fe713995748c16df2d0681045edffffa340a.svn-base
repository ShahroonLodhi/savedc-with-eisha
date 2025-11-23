using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class ListAttachments : Page
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

                // dont allow operator to edit.
                if (SaveDCSession.UserAccessLevel == UserAccessLevels.Operator ||
                    SaveDCSession.UserAccessLevel == UserAccessLevels.Donor)
                {
                    btnDelReport.Visible = false;

                    if (SaveDCSession.UserAccessLevel == UserAccessLevels.Donor)
                    {
                        btnEditReport.Visible = false;
                        trAddReport.Visible = false;
                    }
                }

                LoadReports(1);
                RenderError(Request.QueryString["status"]);
            }
            delPopulateData delPopulate = LoadReports;
            pagerApps.UpdatePageIndex = delPopulate;
        }

        private void LoadReports(int nCurrentPage)
        {
            string szSrchYearName = "";
            if (!string.IsNullOrEmpty(Request.QueryString["Session"]))
            {
                szSrchYearName = Request.QueryString["Session"];
                txtYearName.Text = szSrchYearName;
            }

            int nStudentId = SaveDCSession.StudentId;

            var oCommon = new Common();

            var oStudent = new Student();
            oStudent.StudentId = nStudentId;
            var oStudentManager = new StudentManager(oStudent);
            oStudent = oStudentManager.Load();
            lblStdName.Text = oStudent.FirstName + " " + oStudent.LastName;

            Int32 nTotalRecord = 0;
            DataSet oSqlDataSet = oCommon.LoadStudentAttachments(nStudentId, nCurrentPage,
                                                                           pagerApps.RecordsPerPage, out nTotalRecord);

            //===============================================================
            pagerApps.TotalRecords = nTotalRecord;
            //===============================================================

            dgReports.DataSource = oSqlDataSet;
            dgReports.DataBind();

            // hide/show grid if no rec found
            if (oSqlDataSet == null || oSqlDataSet.Tables[0].Rows.Count == 0)
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
                lblTotal.Text = oSqlDataSet.Tables[0].Rows.Count.ToString();
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

        protected void dgReports_Databound(object sender, DataGridItemEventArgs e)
        {
        }

        protected void btnEditReport_Click(object sender, EventArgs e)
        {
            string szCurReportId = hdnReportID.Value;
            Response.Redirect("AddAttachment.aspx?ReportId=" + szCurReportId);
        }

        protected void btnDetail_Click(object sender, EventArgs e)
        {
            string szCurReportId = hdnReportID.Value;
            Response.Redirect("DetailsAttachment.aspx?ReportId=" + szCurReportId);
        }

        protected void btnDelReport_Click(object sender, EventArgs e)
        {
            string szCurReportId = hdnReportID.Value;

            string szStatus = "5032010";
            var oCommon = new Common();
            int nStatus = oCommon.DeleteStudentAttachment(szCurReportId);
            if (nStatus > 0)
            {
                szStatus = "5032011";
            }

            Response.Redirect("ListAttachments.aspx?status=" + szStatus);
        }

        protected void searchbtn_Click(object sender, ImageClickEventArgs e)
        {
            string szSrchMonthName = "";
            if (!string.IsNullOrEmpty(txtYearName.Text))
                szSrchMonthName = txtYearName.Text;

            Response.Redirect("ListAnnualProgressReports.aspx?Session=" + szSrchMonthName);
        }
    }
}