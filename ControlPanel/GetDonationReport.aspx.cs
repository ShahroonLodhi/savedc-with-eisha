using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Utils;
using System.Globalization;
using System.Threading;
using OfficeOpenXml;
using System.Text.RegularExpressions;

namespace SaveDC.ControlPanel
{
    public partial class GetDonationReport : Page
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
                                                  new[] {UserAccessLevels.SuperAdmin, UserAccessLevels.Admin});
                oValidator = null;

                GetReport(1);
                RenderError(Request.QueryString["status"]);
            }
            delPopulateData delPopulate = GetReport;
            pagerApps.UpdatePageIndex = delPopulate;
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

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (Session["GeneralDonationReport"] != null)
            {
                DataSet ds = Session["GeneralDonationReport"] as DataSet;
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        ExportToExcel(ds.Tables[0], "General Donation Report");
                    }
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
                    dt2.Columns.Remove("IID");
                    dt2.Columns.Remove("DonorID");
                    dt2.Columns.Remove("PhoneNum");
                    dt2.Columns["DonorName"].ColumnName = "Donor Name";
                    dt2.Columns["FirstName"].ColumnName = "First Name";
                    dt2.Columns["LastName"].ColumnName = "Last Name";
                    dt2.Columns["TotalDonations"].ColumnName = "Total Donations";
                    dt2.Columns["AmountUsed"].ColumnName = "Total Expenses";
                    dt2.Columns["Balnace"].ColumnName = "Balance";
                    dt2.Columns["TotalStudents"].ColumnName = "Total Students";

                    dt2.Columns["Total Students"].SetOrdinal(5);
                    dt2.Columns["Balance"].SetOrdinal(6);

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

        private void GetReport(int nCurrentPage)
        {
            Int32 nTotalRecord = 0;
            string szDonorName = Utils.fixNullString(Request.QueryString["DonorName"]);
            txtUserName.Text = szDonorName;

            var oCommon = new Common();
            DataSet report = oCommon.GetDonationReport(szDonorName, nCurrentPage, pagerApps.RecordsPerPage,
                                                       out nTotalRecord);
            Session["GeneralDonationReport"] = report;
            //===============================================================
            pagerApps.TotalRecords = nTotalRecord;
            //===============================================================

            dgUsers.DataSource = report;
            dgUsers.DataBind();

            if (report == null || report.Tables == null || report.Tables[0].Rows.Count <= 0)
            {
                // set the total
                lblTotal.Text = 0.ToString();
                tbDataFound.Visible = false;
                tbNoDataFound.Visible = true;
            }
            else
            {
                lblTotal.Text = report.Tables[0].Rows.Count.ToString();
                tbDataFound.Visible = true;
                tbNoDataFound.Visible = false;
            }
        }

        protected void searchbtn_Click(object sender, ImageClickEventArgs e)
        {
            string szSrchDonorName = "";
            if (!string.IsNullOrEmpty(txtUserName.Text))
                szSrchDonorName = txtUserName.Text;

            Response.Redirect("GetDonationReport.aspx?DonorName=" + szSrchDonorName);
        }

        protected void dgUsers_Databound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //decimal nBalanceId = Utils.fixNullDecimal(((HiddenField) e.Item.FindControl("Balnace")).Value);
                string nBalance = Utils.fixNullString(((HiddenField)e.Item.FindControl("Balnace")).Value);
                decimal AmountToCheck = Convert.ToDecimal(nBalance.Replace("Rs.", "").Replace("\t", "").Replace(" ",""));
                if (AmountToCheck > 0)
                {
                    e.Item.Cells[5].Text = "<font color = 'green'>" + nBalance.ToString() + "</font>";
                }
                else if (AmountToCheck < 0)
                {
                    e.Item.Cells[5].Text = "<font color = 'red'>" + nBalance.ToString() + "</font>";
                }
            }
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