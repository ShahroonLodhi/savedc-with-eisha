using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class PrintDetailsMonthlyExpense : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // page validation

                // load edit user details.
                var oCommon = new Common();
                int ExpenseId = Utils.fixNullInt(Request.QueryString["ExpenseId"]);
                if (ExpenseId > 0)
                {
                    Expense expenseDetails = oCommon.GetExpenseDetails(ExpenseId);

                    lblFileNum.Text = expenseDetails.FileNum;
                    lblVoucherNum.Text = expenseDetails.VoucherNum;

                    hdnExpenseID.Value = expenseDetails.ExpenseId.ToString();
                    lblExpenseType.Text = expenseDetails.ExpenseName;// Type == "1" ? "School Expense" : "Other Expense";
                    lblPostedOn.Text = expenseDetails.ActionDate.ToString("dd MMM yyyy");
                    //lblExpensePostedBy.Text = expenseDetails.PostedByName.ToString();

                    lblMonth.Text = String.Format("{0:y}", expenseDetails.Month);

                    if (expenseDetails.ExpenseType == "1")
                    {
                        PlaceHolderOther.Visible = false;
                        PlaceHolderSchool.Visible = true;

                        lblSchoolName.Text = oCommon.GetSchoolNameById(expenseDetails.SchoolId);

                        var oStudent = new Student();
                        oStudent.SchoolId = expenseDetails.SchoolId;
                        var oStudentManager = new StudentManager(oStudent);
                        Student[] students = oStudentManager.GetStudents(0);
                        Repeater1.DataSource = students;
                        Repeater1.DataBind();

                        DataSet oSqlData = oCommon.LoadMonthlyExpenseCats();
                        Repeater3.DataSource = oSqlData;
                        Repeater3.DataBind();

                        if (oSqlData != null)
                        {
                            hdnColSpan.Value = (oSqlData.Tables[0].Rows.Count + 1).ToString();
                        }
                    }
                    else
                    {
                        PlaceHolderOther.Visible = true;
                        PlaceHolderSchool.Visible = false;

                        lblPayee.Text = expenseDetails.ExpensePayee;
                        lblAmount.Text = expenseDetails.ExpenseAmount.ToString();
                        lblDetail.Text = expenseDetails.ExpenseDetail;

                        lblPrincipalSign.Visible = false;
                        hdnColSpan.Value = "1";
                    }

                    if (expenseDetails.PaymentMode.ToString() == "1")
                    {
                        txtPaymentMode.Text = "Cheque";
                        PlaceHolder1.Visible = true;
                    }
                    else
                    {
                        txtPaymentMode.Text = "Cash";
                        PlaceHolder1.Visible = true;
                    }

                    txtBank.Text = expenseDetails.BankName;
                    txtAccNum.Text = expenseDetails.BenefiName;
                    txtChkNum.Text = expenseDetails.ChequeNum;
                    txtNote.Text = expenseDetails.PaymentNote;
                }
            }
        }


        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    string studentId = ((HiddenField) e.Item.FindControl("hdnStudentId")).Value;

                    var childrepeatter = (Repeater) e.Item.FindControl("Repeater2");

                    string ExpesnseId = hdnExpenseID.Value;
                    var oCommon = new Common();
                    DataSet oSqlData = oCommon.LoadSchoolExpenseDetails(studentId, Utils.fixNullInt(ExpesnseId));

                    childrepeatter.DataSource = oSqlData;
                    childrepeatter.DataBind();

                    //Label txtboxAmount = (Label)e.Item.FindControl("txtTotal");
                    //txtboxAmount.Attributes.Add("name", "txtTotal" + "_" + studentId );
                    //RadioButtonList radioButtonVoteItem = (RadioButtonList)e.Item.FindControl("chkRemarks");
                    //string selectedRemarks = ((HiddenField)e.Item.FindControl("hdnPrevRemarks")).Value;
                    //radioButtonVoteItem.SelectedValue = selectedRemarks;
                }
            }
            catch (Exception exception)
            {
            }
        }

        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }
    }
}