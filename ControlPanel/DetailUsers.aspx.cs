using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class DetailUsers : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // page validation

                // get form/query string values.
                int nEditUserId = Utils.fixNullInt(Request.QueryString["UserId"]);

                // load edit user details.
                if (nEditUserId > 0)
                {
                    var oUser = new User();
                    oUser.UserID = nEditUserId;
                    var oUserManager = new UserManager(oUser);
                    oUser = oUserManager.Load();
                    txtUserName.Text = oUser.UserName;

                    txtUserRoles.Text = new Common().GetRoleNameById(oUser.UserRoleID);
                    hdnId.Value = nEditUserId.ToString();

                    if (txtUserRoles.Text.ToLower() == "member")
                    {
                        hdnArea.Value = "Members Area";
                        hdnRole.Value = "Member";
                        trRoles.Visible = false;

                        loginRow1.Visible = false;
                        loginRow2.Visible = false;
                        loginRow3.Visible = false;
                        loginRow4.Visible = false;

                        trGender.Visible = true;
                        trCNIC.Visible = true;
                        trRD.Visible = true;
                        trOccupation.Visible = true;
                        trQualification.Visible = true;
                    }
                    else if (txtUserRoles.Text.ToLower() == "donor")
                    {
                        hdnArea.Value = "Donor Manager";
                        hdnRole.Value = "Donor";
                        trRoles.Visible = false;

                        loginRow5.Visible = false;
                        loginRow6.Visible = false;
                    }
                    else
                    {
                        hdnArea.Value = "User Manager";
                        hdnRole.Value = "User";

                        loginRow5.Visible = false;
                        loginRow6.Visible = false;
                    }

                    txtFName.Text = oUser.FirstName;
                    txtLName.Text = oUser.LastName;
                    txtEmail.Text = oUser.EmailAddress;
                    txtPhone.Text = oUser.PhoneNumber;
                    txtNote.Text = oUser.Notes;
                    lblAddress.Text = oUser.Address;
                    lblCountry.Text = oUser.Country;

                    txtGender.Text = (oUser.Gender)? "Male" : "Female";
                    txtCNIC.Text = oUser.CNIC;
                    if (oUser.RecevingDate != null && oUser.RecevingDate != "")
                        txtReceivingDate.Text = Convert.ToDateTime(oUser.RecevingDate).ToString("dd MMM, yyyy");
                    txtOccupation.Text = oUser.Occupation;
                    txtQualification.Text = oUser.Qualification;

                    LoadDonorsRemarks();
                    LoadDonorsNotes();
                    LoadMembersFees();
                }
            }
        }

        private void LoadDonorsRemarks()
        {
            int nDonorId;
            nDonorId = Utils.fixNullInt(Request.QueryString["UserId"]);

            var common = new Common();
            //hdnDonorName.Value = common.GetDonorNameById(nDonorId);

            DataTable oDonorsRemarks = common.GetDonorsRemarks(nDonorId);

            //===============================================================
            //pagerApps.TotalRecords = nTotal;
            //===============================================================

            dgSchools.DataSource = oDonorsRemarks;
            dgSchools.DataBind();


            // hide/show grid if no rec found
            if (oDonorsRemarks == null || oDonorsRemarks.Rows.Count <= 0)
            {
                // set the total
                tbDataFound.Visible = false;
                tbNoDataFound.Visible = true;
            }
            else
            {
                // set the total
                tbDataFound.Visible = true;
                tbNoDataFound.Visible = false;
            }
        }

        private void LoadDonorsNotes()
        {
            int nDonorId;
            nDonorId = Utils.fixNullInt(Request.QueryString["UserId"]);

            var common = new Common();
            //hdnDonorName.Value = common.GetDonorNameById(nDonorId);

            DataTable oDonorsNotes = common.GetDonorsNotes(nDonorId);

            //===============================================================
            //pagerApps.TotalRecords = nTotal;
            //===============================================================

            dgNotes.DataSource = oDonorsNotes;
            dgNotes.DataBind();


            // hide/show grid if no rec found
            if (oDonorsNotes == null || oDonorsNotes.Rows.Count <= 0)
            {
                // set the total
                tbNotesFound.Visible = false;
                tbNoNotesFound.Visible = true;
            }
            else
            {
                // set the total
                tbNotesFound.Visible = true;
                tbNoNotesFound.Visible = false;
            }
        }

        private void LoadMembersFees()
        {
            int nMemberId;
            nMemberId = Utils.fixNullInt(Request.QueryString["UserId"]);

            var common = new Common();
            //hdnDonorName.Value = common.GetDonorNameById(nDonorId);

            DataTable oMembersFee = common.GetMembersFees(nMemberId);

            //===============================================================
            //pagerApps.TotalRecords = nTotal;
            //===============================================================

            dgFees.DataSource = oMembersFee;
            dgFees.DataBind();


            // hide/show grid if no rec found
            if (oMembersFee == null || oMembersFee.Rows.Count <= 0)
            {
                // set the total
                tbFeeFound.Visible = false;
                tbNoFeeFound.Visible = true;
            }
            else
            {
                // set the total
                tbFeeFound.Visible = true;
                tbNoFeeFound.Visible = false;
            }
        }

        protected void dgSchools_Databound(object sender, DataGridItemEventArgs e)
        {
        }

        protected void dgFees_Databound(object sender, DataGridItemEventArgs e)
        {
        }

        protected void dgNotes_Databound(object sender, DataGridItemEventArgs e)
        {
        }

    }
}