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
    public partial class GetSchoolDonationReport : Page
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
            if (Session["MonthlyDonationReport"] != null)
            {
                DataSet ds = Session["MonthlyDonationReport"] as DataSet;
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        ExportToExcel(ds.Tables[0], "Monthly Donation Report");
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
                    dt2.Columns["DonationMonth"].ColumnName = "Donation Month";
                    dt2.Columns["DonationAmount"].ColumnName = "Total Amount";
                    

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

            string szActionDate = Utils.fixNullString(Request.QueryString["ActionDate"]);

            var oCommon = new Common();
            DataSet report = oCommon.GetSchoolDonationReport(nCurrentPage, pagerApps.RecordsPerPage, szActionDate,
                                                            out nTotalRecord);
            Session["MonthlyDonationReport"] = report;
            //===============================================================
            pagerApps.TotalRecords = nTotalRecord;
            //===============================================================

            dgSchools.DataSource = report;
            dgSchools.DataBind();

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


        protected void dgSchools_Databound(object sender, DataGridItemEventArgs e)
        {
        }

        protected void searchbtn_Click(object sender, ImageClickEventArgs e)
        {
            string szSrchDate = "";
            if (!string.IsNullOrEmpty(txtActionDate.Text))
                szSrchDate = txtActionDate.Text;

            Response.Redirect("GetSchoolDonationReport.aspx?ActionDate=" + szSrchDate);
        }
    }
}