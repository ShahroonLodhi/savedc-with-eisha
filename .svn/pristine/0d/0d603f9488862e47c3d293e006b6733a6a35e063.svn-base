using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class DonorsNotes : Page
    {
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
                                                          UserAccessLevels.Donor
                                                      });

                LoadDonorsNotes();
            }
        }

        private void LoadDonorsNotes()
        {
            int nDonorId;
            nDonorId = SaveDCSession.UserId;

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

        protected void dgNotes_Databound(object sender, DataGridItemEventArgs e)
        {
        }

    }
}