using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;
using System.Globalization;
using System.Threading;
using System.Data;
using System.Text.RegularExpressions;
using OfficeOpenXml;

namespace SaveDC.ControlPanel
{
    public partial class ListFunds : Page
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

                if (SaveDCSession.UserName.ToLower() == "irfan" ||
                    SaveDCSession.UserName.ToLower() == "shamsa" ||
                    SaveDCSession.UserName.ToLower() == "sadaf")
                    HttpContext.Current.Response.Redirect("Login.aspx?status=5011300");

                // dont allow operator to edit.
                if (SaveDCSession.UserAccessLevel == UserAccessLevels.Admin ||
                    //SaveDCSession.UserAccessLevel == UserAccessLevels.Operator ||
                    SaveDCSession.UserAccessLevel == UserAccessLevels.Donor)
                {
                    trAddFund.Visible = false;
                    btnDelFund.Visible = false;
                    btnEditFund.Visible = false;

                    if (SaveDCSession.UserAccessLevel != UserAccessLevels.Donor)
                        btnDetailsFunds.Visible = false;
                }
                if (SaveDCSession.UserAccessLevel == UserAccessLevels.Donor)
                {
                    trAddFund.Visible = false;
                    trSearch.Visible = false;
                }
                //hiding export to excel button for Operator
                if(SaveDCSession.UserAccessLevel == UserAccessLevels.Operator)
                {
                    btnExportToExcel.Visible = false;
                }
                LoadFunds(1);

                RenderError(Request.QueryString["status"]);
            }
            delPopulateData delPopulate = LoadFunds;
            pagerApps.UpdatePageIndex = delPopulate;
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            var oFund = new Fund();
            var oFundManager = new FundManager(oFund);
            DataSet ds = oFundManager.GetFundsInDS();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    ExportToExcel(ds.Tables[0], "Funds Report");
                }
            }

        }

        protected string ConvertAmounToPkrCurrency(object Amount)
        {
            string rtrVal = Convert.ToDecimal(Amount).ToString("C", CultureInfo.CreateSpecificCulture("ur-PK"));
            if (rtrVal.Contains("-"))
            {
                rtrVal = rtrVal.Replace('-', '\t');
                rtrVal = rtrVal.Insert(0, "-");

            }
            rtrVal = rtrVal.Insert(rtrVal.LastIndexOf('s') + 1, ".");
            return rtrVal;
        }

        private void ExportToExcel(DataTable dt, string fileName)
        {
            #region Export Datatable to excel
            try
            {
                if (dt.Rows.Count >= 1)
                {
                    DataTable dt2 = dt.Copy();
                    dt2.Columns.Remove("IID");
                    dt2.Columns.Remove("FundID");
                    dt2.Columns.Remove("DonorID");
                    dt2.Columns.Remove("AttachGUID");
                    dt2.Columns.Remove("FundPostedName");
                    dt2.Columns["TotalAmount"].ColumnName = "Donation Amount";
                    dt2.Columns["DonorName"].ColumnName = "Donor Name";
                    dt2.Columns["DatePaid"].ColumnName = "Posted On";
                    dt2.Columns["Notes"].ColumnName = "Notes/Remarks";
                    dt2.Columns["Donation Amount"].SetOrdinal(0);
                    dt2.Columns["Donor Name"].SetOrdinal(1);
                    dt2.Columns["Posted On"].SetOrdinal(2);
                    dt2.Columns["Notes/Remarks"].SetOrdinal(3);
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

        private void LoadFunds(int nCurrentPage)
        {
            string szSrchDonorName = "", szFundedOn = "";

            if (!string.IsNullOrEmpty(Request.QueryString["DonorName"]))
                szSrchDonorName = Request.QueryString["DonorName"];
            if (!string.IsNullOrEmpty(Request.QueryString["FundedOn"]))
                szFundedOn = Request.QueryString["FundedOn"];

            int nDonorId = 0, nPostedBy = 0;
            if (SaveDCSession.UserAccessLevel == UserAccessLevels.Donor)
                nDonorId = SaveDCSession.UserId;
            if (SaveDCSession.UserAccessLevel == UserAccessLevels.Operator)
                nPostedBy = SaveDCSession.UserId;

            var oFund = new Fund();
            oFund.DonorName = szSrchDonorName;
            oFund.DonorID = nDonorId;
            oFund.FundPostedBy = nPostedBy;
            oFund.FundedOnDateString = szFundedOn;

            var oFundManager = new FundManager(oFund);
            Fund[] funds = oFundManager.GetFunds(nCurrentPage, pagerApps.RecordsPerPage);

            //===============================================================
            pagerApps.TotalRecords = Convert.ToInt32(oFundManager.RecordCount);
            //===============================================================

            dgFunds.DataSource = funds;
            dgFunds.DataBind();


            // hide/show grid if no rec found
            if (funds == null || funds.Length <= 0)
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

                if (SaveDCSession.UserAccessLevel == UserAccessLevels.Donor && dgFunds.Columns.Count > 4)
                    dgFunds.Columns.RemoveAt(2);

                // set the total
                lblTotal.Text = funds.Length.ToString();
            }
        }

        private void RenderError(string szErrorCode)
        {
            if (string.IsNullOrEmpty(szErrorCode))
            {
                lblError.Text = "";
                return;
            }

            //string szErrorDesc = Utils.GetMessageText(szErrorCode);
            string szErrorDesc = String.Empty;
            if (szErrorCode.Equals("5071001"))
            {
                szErrorDesc = "Funds added successfully.";
            }
            else if (szErrorCode.Equals("5071011"))
            {
                szErrorDesc = "Funds updated successfully.";
            }
            else if (szErrorCode.Equals("5071020"))
            {
                szErrorDesc = "Error occured while deleting funds.";
            }
            else if (szErrorCode.Equals("5071021"))
            {
                szErrorDesc = "Funds deleted successfully.";
            }
            else if (szErrorCode.Equals("5071000"))
            {
                szErrorDesc = "Error occured while adding funds.";
            }
            if (!szErrorCode.EndsWith("1"))
                lblError.CssClass = "FailureMessage";
            else
                lblError.CssClass = "SuccessMessage";

            lblError.Text = szErrorDesc;
        }

        protected void DoAction(object source, DataGridCommandEventArgs e)
        {
            string szCommandName = e.CommandName;
            int nFundID = int.Parse(e.CommandArgument.ToString());

            if (szCommandName == "EDIT")
                Response.Redirect("AddFund.aspx?FundId=" + nFundID.ToString());
            else if (szCommandName == "DELETE")
            {
                var oFund = new Fund();
                oFund.FundID = nFundID;

                string szStatus = "5071020";
                var oFundManager = new FundManager(oFund);
                if (oFundManager.Delete())
                {
                    szStatus = "5071021";
                }

                Response.Redirect("ListFunds.aspx?status=" + szStatus);
            }
        }

        protected void searchbtn_Click(object sender, ImageClickEventArgs e)
        {
            string szSrchFundName = txtFundName.Text;
            //if (!string.IsNullOrEmpty(Request.Form["ctl00$ContentPlaceHolder1$txtFundName"]))
            //    szSrchFundName = Request.Form["ctl00$ContentPlaceHolder1$txtFundName"];

            Response.Redirect("ListFunds.aspx?DonorName=" + szSrchFundName + "&FundedOn=" + txtFundedOn.Text);
        }

        protected void dgFunds_Databound(object sender, DataGridItemEventArgs e)
        {
        }

        protected void btnEditFund_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szCurFundId = hdnFundID.Value;
            Response.Redirect("AddFund.aspx?FundId=" + szCurFundId);
        }

        protected void btnDelFund_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szCurFundId = hdnFundID.Value;

            var oFund = new Fund();
            oFund.FundID = int.Parse(szCurFundId);

            string szStatus = "5071020";
            var oFundManager = new FundManager(oFund);
            if (oFundManager.Delete())
            {
                szStatus = "5071021";
            }

            Response.Redirect("ListFunds.aspx?status=" + szStatus);
        }

        protected void btnDetailsFunds_Click(object sender, EventArgs e)
        {
            string szCurFundId = hdnFundID.Value;
            Response.Redirect("DetailsFund.aspx?FundId=" + szCurFundId);
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