using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class DonorMonthlyExpReport : Page
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
                oValidator.ValidateUserPageAccess(SaveDCSession.UserAccessLevel, new[] {UserAccessLevels.Donor});
                oValidator = null;

                GetReport(1);
            }
            delPopulateData delPopulate = GetReport;
            pagerApps.UpdatePageIndex = delPopulate;
        }

        private void GetReport(int nCurrentPage)
        {
            Int32 nTotalRecord = 0;
            // string szDonorName = Utils.fixNullString(Request.QueryString["DonorName"]);
            // txtUserName.Text = szDonorName;

            var oCommon = new Common();
            DataSet report = oCommon.GetDonorMonthlyReport(SaveDCSession.UserId, nCurrentPage, pagerApps.RecordsPerPage,
                                                           out nTotalRecord);

            //===============================================================
            pagerApps.TotalRecords = nTotalRecord;
            //===============================================================

            dgUsers.DataSource = report;
            dgUsers.DataBind();

            if (report == null || report.Tables == null || report.Tables[0].Rows.Count <= 0)
            {
                // set the total
                // lblTotal.Text = 0.ToString();
                tbDataFound.Visible = false;
                tbNoDataFound.Visible = true;
            }
            else
            {
                // lblTotal.Text = report.Tables[0].Rows.Count.ToString();
                tbDataFound.Visible = true;
                tbNoDataFound.Visible = false;
            }
        }


        protected void searchbtn_Click(object sender, ImageClickEventArgs e)
        {
            //string szSrchDonorName = "";
            //if (!string.IsNullOrEmpty(txtUserName.Text))
            //    szSrchDonorName = txtUserName.Text;

            //Response.Redirect("GetDonationReport.aspx?DonorName=" + szSrchDonorName);
        }

        protected void dgUsers_Databound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                decimal nBalanceId = Utils.fixNullDecimal(((HiddenField) e.Item.FindControl("Balnace")).Value);
                if (nBalanceId > 0)
                {
                    e.Item.Cells[2].Text = "<font color = 'green'>Paid</font>";
                }
                else if (nBalanceId < 0)
                {
                    e.Item.Cells[2].Text = "<font color = 'red'>Awaiting Payment</font>";
                }
            }
        }
    }
}