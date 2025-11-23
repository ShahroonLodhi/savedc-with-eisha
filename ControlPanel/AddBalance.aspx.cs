using System;
using System.Web.UI;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;
using SaveDC.ControlPanel.Src.DB;

namespace SaveDC.ControlPanel
{
    public partial class AddBalance : Page
    {
        public AddBalance()
        {
            PreInit += AddBalance_PreInit;
            // this.Load += new EventHandler(AddStudentEstimation_Load);
        }

        private void AddBalance_PreInit(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["modalwin"]) && Request.QueryString["modalwin"] == "1")
                MasterPageFile = "~/ControlPanel/Dummy.master";
        }

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
                oValidator = null;

                // dont allow operator to edit.
                if (SaveDCSession.UserAccessLevel != UserAccessLevels.Operator)
                {
                    // get form/query string values.
                    int nEditBalanceId = Utils.fixNullInt(Request.QueryString["BalanceId"]);

                    // load edit user details.
                    if (nEditBalanceId > 0)
                    {
                        var fundDAO = new FundDAO();
                        Balance oBalance = fundDAO.LoadBalance(nEditBalanceId);
                        txtABL.Text = oBalance.ABL.ToString();
                        txtFBL.Text = oBalance.FBL.ToString();

                        hdnEditSchoolId.Value = nEditBalanceId.ToString();
                        hdnAddEdit.Value = "Edit";
                    }
                }
                RenderError(Request.QueryString["status"]);
            }
        }

        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            var oBalance = new Balance();
            oBalance.ABL = Utils.fixNullDecimal(txtABL.Text);
            oBalance.FBL = Utils.fixNullDecimal(txtFBL.Text);

            int nEditBalanceId = Utils.fixNullInt(Request.QueryString["BalanceId"]);

            var fundDAO = new FundDAO();
            int nStatus = fundDAO.AddBalance(nEditBalanceId, oBalance.ABL, oBalance.FBL);

            if (nStatus > 0)
            {
                if (nEditBalanceId > 0)
                    Response.Redirect("ListBalance.aspx?status=5120021"); //  balance updated successfully.
                else
                    Response.Redirect("ListBalance.aspx?status=5120011");
            }
            else
            {
                Response.Redirect("AddBalance.aspx?status=5120010");
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
    }
}