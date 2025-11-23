using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class ListDonorSms : Page
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
                                                      });

                LoadSchoolSMSs(1);
            }
            delPopulateData delPopulate = LoadSchoolSMSs;
            pagerApps.UpdatePageIndex = delPopulate;
        }

        private void LoadSchoolSMSs(int nCurrentPage)
        {
            int nDonorId;
            nDonorId = Utils.fixNullInt(Request.QueryString["DonorId"]);

            var common = new Common();
            hdnDonorName.Value = common.GetDonorNameById(nDonorId);

            int nTotal;
            DataTable oSchoolSms = common.GetSentSms(nDonorId, "DON", nCurrentPage, 100, "", out nTotal);

            //===============================================================
            pagerApps.TotalRecords = nTotal;
            //===============================================================

            dgSchools.DataSource = oSchoolSms;
            dgSchools.DataBind();


            // hide/show grid if no rec found
            if (oSchoolSms == null || oSchoolSms.Rows.Count <= 0)
            {
                // set the total
                lblTotal.Text = "0";
                tbDataFound.Visible = false;
                tbNoDataFound.Visible = true;
            }
            else
            {
                // set the total
                lblTotal.Text = oSchoolSms.Rows.Count.ToString();
                tbDataFound.Visible = true;
                tbNoDataFound.Visible = false;
            }
        }

        protected void dgSchools_Databound(object sender, DataGridItemEventArgs e)
        {
        }
    }
}