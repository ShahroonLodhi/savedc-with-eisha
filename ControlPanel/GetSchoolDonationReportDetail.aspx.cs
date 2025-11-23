using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class GetSchoolDonationReportDetail : Page
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
            if (Session["MonthlyDonationReportDetail"] != null)
            {
                DataSet ds = Session["MonthlyDonationReportDetail"] as DataSet;
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        ExportToExcel(ds.Tables[0], "Monthly Donation Report for " + lblMonth.Text);
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
                    dt2.Columns.Remove("Attachment");
                    dt2.Columns["DonorName"].ColumnName = "Donor Name";
                    dt2.Columns["TotalDonations"].ColumnName = "Donation Amount";
                    dt2.Columns["PaymentDate"].ColumnName = "Posted On";
                    dt2.Columns["Notes"].ColumnName = "Notes/Remarks";




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

        private void GetReport(int nCurrentPage)
        {
            Int32 nTotalRecord = 0;
            string szSchoolName = Utils.fixNullString(Request.QueryString["SchoolName"]);
            string szExpMonth = Utils.fixNullString(Request.QueryString["DonMonth"]);
            hdnExpMonth.Value = szExpMonth;
            lblMonth.Text = szExpMonth;
            txtSchoolName.Text = szSchoolName;

            var oCommon = new Common();
            DataSet report = oCommon.GetSchoolMonthlyDonationReportDetail(szSchoolName, szExpMonth, nCurrentPage,
                                                                         pagerApps.RecordsPerPage,
                                                                         out nTotalRecord);
            Session["MonthlyDonationReportDetail"] = report;
            //===============================================================
            pagerApps.TotalRecords = nTotalRecord;
            //===============================================================

            dgSchools.DataSource = report;
            dgSchools.DataBind();

            if (report == null || report.Tables[0].Rows.Count <= 0)
            {
                // set the total
                lblTotal.Text = "0";
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
            string szSrchSchoolName = "";
            if (!string.IsNullOrEmpty(txtSchoolName.Text))
                szSrchSchoolName = txtSchoolName.Text;

            Response.Redirect("GetSchoolDonationReportDetail.aspx?SchoolName=" + szSrchSchoolName + "&ExpMonth=" +
                              hdnExpMonth.Value);
        }

        protected void dgSchools_Databound(object sender, DataGridItemEventArgs e)
        {
        }
    }
}