using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class ListMarkettingSMS : Page
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
                //hiding export to excel button for Operator
                if (SaveDCSession.UserAccessLevel == UserAccessLevels.Operator)
                {
                    btnExportToExcel.Visible = false;
                }

                LoadSMSs(1);
                RenderError(Request.QueryString["status"]);
            }
            delPopulateData delPopulate = LoadSMSs;
            pagerApps.UpdatePageIndex = delPopulate;
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (Session["SMSDS"] != null)
            {
                DataTable ds = Session["SMSDS"] as DataTable;
                if (ds != null && ds.Rows.Count > 0)
                {
                    if (ds != null && ds.Rows.Count > 0)
                    {
                        ExportToExcel(ds, "Bulk SMS Report");
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
                    dt2.Columns.Remove("SmsId");
                    dt2.Columns.Remove("RecipientNum");
                    dt2.Columns.Remove("ParentId");
                    dt2.Columns.Remove("ParentType");
                    dt2.Columns["MessageText"].ColumnName = "SMS Text";
                    dt2.Columns["RecipientCount"].ColumnName = "Total Recipeints";
                    dt2.Columns["ActionDate"].ColumnName = "Sent On";


                    dt2.Columns["Total Recipeints"].SetOrdinal(1);
                    dt2.Columns["Sent On"].SetOrdinal(2);
                    
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
            if (szErrorCode.Equals("9999999"))
            {
                lblError.CssClass = "FailureMessage";
                lblError.Text = "Operator can only send local(+92) SMS";
            }
            else
            {
                string szErrorDesc = Utils.GetMessageText(szErrorCode);
                if (!szErrorCode.EndsWith("1"))
                    lblError.CssClass = "FailureMessage";
                else
                    lblError.CssClass = "SuccessMessage";

                lblError.Text = szErrorDesc;
            }
            
        }

        private void LoadSMSs(int nCurrentPage)
        {
            var common = new Common();
            string szActionDate = Utils.fixNullString(Request.QueryString["ActionDate"]);

            int nTotal;
            DataTable oMarSms = common.GetSentSms(0, "MAR", nCurrentPage, 100, szActionDate, out nTotal);
            //Hiding the Data in case of operator
            if(SaveDCSession.UserAccessLevel == UserAccessLevels.Operator)
            {
                oMarSms = null;
            }
            Session["SMSDS"] = oMarSms;
            //===============================================================
            pagerApps.TotalRecords = nTotal;
            //===============================================================

            dgSchools.DataSource = oMarSms;
            dgSchools.DataBind();


            // hide/show grid if no rec found
            if (oMarSms == null || oMarSms.Rows.Count <= 0)
            {
                // set the total
                lblTotal.Text = "0";
                tbDataFound.Visible = false;
                tbNoDataFound.Visible = true;
            }
            else
            {
                // set the total
                lblTotal.Text = oMarSms.Rows.Count.ToString();
                tbDataFound.Visible = true;
                tbNoDataFound.Visible = false;
            }
        }

        protected void dgSchools_Databound(object sender, DataGridItemEventArgs e)
        {
        }

        protected void searchbtn_Click(object sender, ImageClickEventArgs e)
        {
            string szSrchSchoolName = "";
            if (!string.IsNullOrEmpty(txtActionDate.Text))
                szSrchSchoolName = txtActionDate.Text;

            Response.Redirect("ListMarkettingSMS.aspx?ActionDate=" + szSrchSchoolName);
        }
    }
}