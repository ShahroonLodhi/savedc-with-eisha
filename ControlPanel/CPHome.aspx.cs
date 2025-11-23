using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class CPHome : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var oCommon = new Common();

                int nDonorId = 0;
                if (SaveDCSession.UserAccessLevel == UserAccessLevels.Donor)
                    nDonorId = SaveDCSession.UserId;

                SqlDataReader reader = oCommon.GetPanelSummary(nDonorId);
                if (reader.Read())
                {
                    if (SaveDCSession.UserAccessLevel == UserAccessLevels.Donor)
                    {
                        PlaceHolder1.Visible = false;
                        //trStudentSummary.Visible = false;
                    }
                    else
                    {
                        lblTotalFamilies.Text = Utils.fixNullString(reader["FamilyCount"]);
                        lblTotalSchools.Text = Utils.fixNullString(reader["SchoolCount"]);
                        lblTotalDonors.Text = Utils.fixNullString(reader["DonorCount"]);
                        lblTotalAdmins.Text = Utils.fixNullString(reader["AdminCount"]);

                        lblTotalOperatorss.Text = Utils.fixNullString(reader["OperatorCount"]);
                        lblStdNew.Text = Utils.fixNullString(reader["NewStdCount"]);
                        lblStdPending.Text = Utils.fixNullString(reader["PendingStdCount"]);
                        lblStdApproved.Text = Utils.fixNullString(reader["ApproStdCount"]);
                        lblStdSponsored.Text = Utils.fixNullString(reader["SponsoredStdCount"]);

                        lblStdRejected.Text = Utils.fixNullString(reader["RejStdCount"]);
                    }
                    lblTotalStd.Text = Utils.fixNullString(reader["StudentCount"]);
                    lblStdDiscontinued.Text = Utils.fixNullString(reader["DiscontinuedStdCount"]);

                    if (SaveDCSession.UserAccessLevel == UserAccessLevels.Donor)
                    {
                        if (lblStdDiscontinued.Text != "")
                        {
                            lblStdSponsored.Text = Convert.ToString(Convert.ToInt32(lblTotalStd.Text.ToString()) - Convert.ToInt32(lblStdDiscontinued.Text.ToString()));
                            PlaceHolder2.Visible = false;
                        }
                        tblFundSumaryTD.RowSpan = 2;
                    }


                    if (SaveDCSession.UserAccessLevel != UserAccessLevels.Operator &&
                        SaveDCSession.UserName.ToLower() != "fabiha" && SaveDCSession.UserName.ToLower() != "nadeem")
                    {
                        string TotalDonations = Convert.ToDecimal(Utils.fixNullString(reader["TotalDonations"])).ToString("C", CultureInfo.CreateSpecificCulture("ur-PK"));
                        if (TotalDonations.Contains("-"))
                        {
                            TotalDonations = TotalDonations.Replace('-', '\t');
                            TotalDonations = TotalDonations.Insert(0, "-");

                        }
                        lblDonations.Text = TotalDonations.Insert(TotalDonations.LastIndexOf('s') + 1 , ".");
                        string TotalExpenses = Convert.ToDecimal(Utils.fixNullString(reader["TotalExpenses"])).ToString("C", CultureInfo.CreateSpecificCulture("ur-PK"));
                        //it means negative value.
                        if (TotalExpenses.Contains("-"))
                        {
                            TotalExpenses = TotalExpenses.Replace('-', '\t');
                            TotalExpenses = TotalExpenses.Insert(0, "-");

                        }
                        lblExpenses.Text = TotalExpenses.Insert(TotalExpenses.LastIndexOf('s') + 1, ".");

                        try
                        {
                            //decimal balance = decimal.Parse(lblDonations.Text) - decimal.Parse(lblExpenses.Text);
                            decimal balance = Convert.ToDecimal(Utils.fixNullString(reader["TotalDonations"])) - Convert.ToDecimal(Utils.fixNullString(reader["TotalExpenses"]));
                            string TotalBalance = balance.ToString("C", CultureInfo.CreateSpecificCulture("ur-PK"));
                            //it means negative value.
                            if (TotalBalance.Contains("-"))
                            {
                                TotalBalance = TotalBalance.Replace('-', '\t');
                                TotalBalance = TotalBalance.Insert(0, "-");

                            }
                            lblBalance.Text = TotalBalance.Insert(TotalBalance.LastIndexOf('s') + 1, ".");
                            //lblBalance.Text = balance.ToString();
                        }
                        catch
                        {
                        }


                        if (SaveDCSession.UserAccessLevel != UserAccessLevels.Donor)
                        {
                            string TotalDonationsLastMonth = Convert.ToDecimal(Utils.fixNullString(reader["TotalDonationsLastMonth"])).ToString("C", CultureInfo.CreateSpecificCulture("ur-PK"));
                            if (TotalDonationsLastMonth.Contains("-"))
                            {
                                TotalDonationsLastMonth = TotalDonationsLastMonth.Replace('-', '\t');
                                TotalDonationsLastMonth = TotalDonationsLastMonth.Insert(0, "-");

                            }
                            lblTotalDonationsLastMonth.Text = TotalDonationsLastMonth.Insert(TotalDonationsLastMonth.LastIndexOf('s') + 1, ".");
                            //lblTotalDonationsLastMonth.Text = Utils.fixNullString(reader["TotalDonationsLastMonth"]);
                            string TotalExpensesLastMonth = Convert.ToDecimal(Utils.fixNullString(reader["TotalExpensesLastMonth"])).ToString("C", CultureInfo.CreateSpecificCulture("ur-PK"));
                            if (TotalExpensesLastMonth.Contains("-"))
                            {
                                TotalExpensesLastMonth = TotalExpensesLastMonth.Replace('-', '\t');
                                TotalExpensesLastMonth = TotalExpensesLastMonth.Insert(0, "-");

                            }
                            //lblExpensesLastMonth.Text = Utils.fixNullString(reader["TotalExpensesLastMonth"]);
                            lblExpensesLastMonth.Text = TotalExpensesLastMonth.Insert(TotalExpensesLastMonth.LastIndexOf('s') + 1, ".");

                            try
                            {
                                //decimal balance = decimal.Parse(lblTotalDonationsLastMonth.Text) -
                                //                  decimal.Parse(lblExpensesLastMonth.Text);
                                decimal BalanceLastMonth = Convert.ToDecimal(Utils.fixNullString(reader["TotalDonationsLastMonth"])) - Convert.ToDecimal(Utils.fixNullString(reader["TotalExpensesLastMonth"]));
                                string TotalBalanceLastMonth = BalanceLastMonth.ToString("C", CultureInfo.CreateSpecificCulture("ur-PK"));
                                //it means negative value.
                                if (TotalBalanceLastMonth.Contains("-"))
                                {
                                    TotalBalanceLastMonth = TotalBalanceLastMonth.Replace('-', '\t');
                                    TotalBalanceLastMonth = TotalBalanceLastMonth.Insert(0, "-");

                                }
                                lblBalanceLastMonth.Text = TotalBalanceLastMonth.Insert(TotalBalanceLastMonth.LastIndexOf('s') + 1, ".");
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            tblFundSumaryLastMonth.Visible = false;
                        }
                    }
                    else
                    {
                        tblFundSumary.Visible = false;
                        tblFundSumaryLastMonth.Visible = false;
                    }

                    if (SaveDCSession.UserAccessLevel == UserAccessLevels.SuperAdmin || SaveDCSession.UserName.ToLower() == "azhar")
                    {
                        tblBalanceSummary.Visible = true;

                        //ABLbalance.Text = Utils.fixNullString(reader["ABLbalance"]);
                        string ABLTotalBalance = Convert.ToDecimal(Utils.fixNullString(reader["ABLbalance"])).ToString("C", CultureInfo.CreateSpecificCulture("ur-PK"));
                        if (ABLTotalBalance.Contains("-"))
                        {
                            ABLTotalBalance = ABLTotalBalance.Replace('-', '\t');
                            ABLTotalBalance = ABLTotalBalance.Insert(0, "-");

                        }
                        ABLbalance.Text = ABLTotalBalance.Insert(ABLTotalBalance.LastIndexOf('s') + 1, ".");
                        //FBLbalance.Text = Utils.fixNullString(reader["FBLbalance"]);
                        string FBLTotalBalance = Convert.ToDecimal(Utils.fixNullString(reader["FBLbalance"])).ToString("C", CultureInfo.CreateSpecificCulture("ur-PK"));
                        if (FBLTotalBalance.Contains("-"))
                        {
                            FBLTotalBalance = FBLTotalBalance.Replace('-', '\t');
                            FBLTotalBalance = FBLTotalBalance.Insert(0, "-");

                        }
                        FBLbalance.Text = FBLTotalBalance.Insert(FBLTotalBalance.LastIndexOf('s') + 1, ".");

                        decimal TotalBankBalance = Convert.ToDecimal(Utils.fixNullString(reader["ABLbalance"])) + Convert.ToDecimal(Utils.fixNullString(reader["FBLbalance"]));
                        string FinalBankBalance = TotalBankBalance.ToString("C", CultureInfo.CreateSpecificCulture("ur-PK"));
                        //it means negative value.
                        if (FinalBankBalance.Contains("-"))
                        {
                            FinalBankBalance = FinalBankBalance.Replace('-', '\t');
                            FinalBankBalance = FinalBankBalance.Insert(0, "-");

                        }
                        totalBalance.Text = FinalBankBalance.Insert(FinalBankBalance.LastIndexOf('s') + 1, ".");
                        //totalBalance.Text = Convert.ToString(Convert.ToDecimal(ABLbalance.Text) + Convert.ToDecimal(FBLbalance.Text));
                    }
                    else
                    {
                        tblBalanceSummary.Visible = false;

                        if (SaveDCSession.UserAccessLevel == UserAccessLevels.Admin || SaveDCSession.UserAccessLevel == UserAccessLevels.Operator)
                        {
                            tblPanelSummaryTD.Width = "100%";
                            tblStudentSummaryTD.Width = "100%";

                            tblPanelSummaryTD.ColSpan = 3;
                            tblStudentSummaryTD.ColSpan = 3;
                        }
                        else if (SaveDCSession.UserAccessLevel == UserAccessLevels.Donor)
                        {
                            tblFundSumaryTD.Width = "50%";
                            tblFundSumaryTD.ColSpan = 2;
                        }
                    }

                    if ((SaveDCSession.UserAccessLevel == UserAccessLevels.SuperAdmin ||
                         SaveDCSession.UserAccessLevel == UserAccessLevels.Admin) && Session["AdjFlag"] == null)
                    {
                        Session["AdjFlag"] = "1";

                        var thread = new Thread(oCommon.AddnAdjustDonorExp);
                        thread.Start();
                    }
                }
            }
        }
    }
}