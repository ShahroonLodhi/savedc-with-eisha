using System;
using System.Web;
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
    public partial class ListMonthlyExpenses : Page
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
                oValidator = null;

                if (SaveDCSession.UserName.ToLower() == "irfan" ||
                    SaveDCSession.UserName.ToLower() == "shamsa" ||
                    SaveDCSession.UserName.ToLower() == "sadaf")
                    HttpContext.Current.Response.Redirect("Login.aspx?status=5011300");

                // dont allow operator to edit.
                if (SaveDCSession.UserAccessLevel == UserAccessLevels.Operator)
                {
                    btnDelExpense.Visible = false;
                    btnEditExpense.Visible = false;
                    btnExportToExcel.Visible = false;
                }
                else if (SaveDCSession.UserName.ToLower() == "azhar" || SaveDCSession.UserName.ToLower() == "nadeem")
                {
                    btnDelExpense.Visible = false;
                    btnEditExpense.Visible = false;
                }

                if (SaveDCSession.UserName.ToLower() == "azhar" || SaveDCSession.UserName.ToLower() == "muzaffar")
                {
                    btnEditExpense.Visible = true;
                }

                LoadExpenses(1);
                RenderError(Request.QueryString["status"]);
            }
            delPopulateData delPopulate = LoadExpenses;
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
            if (Session["ExpenseDS"] != null)
            {
                DataSet ds = Session["ExpenseDS"] as DataSet;
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        ExportToExcel(ds.Tables[0], "Expense Report");
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
                    dt2.Columns.Remove("ExpenseID");
                    dt2.Columns.Remove("SchoolID");
                    dt2.Columns.Remove("ExpenseType");
                    dt2.Columns.Remove("ExpenseDetail");
                    dt2.Columns.Remove("ChequeNum");
                    dt2.Columns.Remove("PaymentMode");
                    dt2.Columns.Remove("BankID");
                    dt2.Columns.Remove("BenefiName");
                    dt2.Columns.Remove("Note");
                    dt2.Columns.Remove("PostedBy");
                    dt2.Columns.Remove("FileNum");
                    dt2.Columns.Remove("VoucherNum");
                    dt2.Columns.Remove("Month");
                    dt2.Columns.Remove("ActionDate");
                    dt2.Columns["ExpensePayee"].ColumnName = "Expense Payee";
                    dt2.Columns["ExpenseAmount"].ColumnName = "E.Amount";
                    dt2.Columns["eType"].ColumnName = "E.Type";
                    dt2.Columns["PayMode"].ColumnName = "P.Mode";
                    dt2.Columns["BankName"].ColumnName = "Bank";
                    dt2.Columns["ExpenseMonthFormatted"].ColumnName = "E.Month";
                    dt2.Columns["ActionDateFormatted"].ColumnName = "Posted On";

                    dt2.Columns["Expense Payee"].SetOrdinal(0);
                    dt2.Columns["E.Amount"].SetOrdinal(1);
                    dt2.Columns["E.Type"].SetOrdinal(2);
                    dt2.Columns["P.Mode"].SetOrdinal(3);
                    dt2.Columns["Bank"].SetOrdinal(4);
                    dt2.Columns["E.Month"].SetOrdinal(5);
                    dt2.Columns["Posted On"].SetOrdinal(6);
                    //dt2.Columns["Donation Amount"].SetOrdinal(0);
                    //dt2.Columns["Donor Name"].SetOrdinal(1);
                    //dt2.Columns["Posted On"].SetOrdinal(2);
                    //dt2.Columns["Notes/Remarks"].SetOrdinal(3);
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
        private void LoadExpenses(int nCurrentPage)
        {
            string szSrchPayeeName = "", szPostedOnDate = "";
            if (!string.IsNullOrEmpty(Request.QueryString["MonthName"]))
            {
                szSrchPayeeName = Request.QueryString["MonthName"];
                txtMonthName.Text = szSrchPayeeName;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["PostedOnDate"]))
            {
                szPostedOnDate = Request.QueryString["PostedOnDate"];
                txtPostedOn.Text = szPostedOnDate;
            }


            int nSchoolId = SaveDCSession.SchoolId;

            var oCommon = new Common();

            // lblSchool.Text = oCommon.GetSchoolNameById(nSchoolId);

            DateTime fromDate, toDate;
            fromDate = DateTime.MinValue;
            toDate = DateTime.MaxValue;

            Int32 nTotalRecord = 0;

            DataSet oSqlDataSet = oCommon.LoadAllExpenses(szSrchPayeeName, szPostedOnDate, fromDate, toDate,
                                                          nCurrentPage, pagerApps.RecordsPerPage, out nTotalRecord);
            //Disabling it based on the new feedback on 1/22/2022
            //if(SaveDCSession.UserAccessLevel == UserAccessLevels.Operator)
            //{
            //    oSqlDataSet = null;
            //}
            Session["ExpenseDS"] = oSqlDataSet;

            //===============================================================
            pagerApps.TotalRecords = nTotalRecord;
            //===============================================================

            dgExpenses.DataSource = oSqlDataSet;
            dgExpenses.DataBind();

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

        protected void dgExpenses_Databound(object sender, DataGridItemEventArgs e)
        {
        }

        protected void btnEditExpense_Click(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szExpense = hdnExpenseId.Value;
            Response.Redirect("AddMonthlyExpense.aspx?ExpenseId=" + szExpense);
        }

        protected void btnDetail_Click(object sender, EventArgs e)
        {
            string szExpense = hdnExpenseId.Value;
            Response.Redirect("DetailsMonthlyExpense.aspx?ExpenseId=" + szExpense);
        }

        protected void btnDelExpense_Click1(object sender, EventArgs e)
        {
            #region User Right Validation.

            // only admin and super admin are allowed to execute this part.
            new Validator().CheckUserRightsOnEditDelete(Request);

            #endregion

            string szMonth = hdnExpenseId.Value;

            string szStatus = "5021030";
            var oCommon = new Common();
            int nStatus = oCommon.DeleteExpense(Utils.fixNullInt(szMonth));
            if (nStatus > 0)
            {
                szStatus = "5021031";
            }

            Response.Redirect("ListMonthlyExpenses.aspx?status=" + szStatus);
        }


        protected void searchbtn_Click(object sender, ImageClickEventArgs e)
        {
            string szSrchMonthName = "", szPostedOnDate = "";
            if (!string.IsNullOrEmpty(txtMonthName.Text))
                szSrchMonthName = txtMonthName.Text;
            if (!string.IsNullOrEmpty(txtPostedOn.Text))
                szPostedOnDate = txtPostedOn.Text;


            Response.Redirect("ListMonthlyExpenses.aspx?MonthName=" + szSrchMonthName + "&PostedOnDate=" +
                              szPostedOnDate);
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