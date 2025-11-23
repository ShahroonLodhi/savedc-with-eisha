using System;
using System.Web.UI;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class DetailsSchool : Page
    {
        public DetailsSchool()
        {
            PreInit += Page_PreInit;
            Load += Page_Load;
        }

        private void Page_PreInit(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["modalwin"]) && Request.QueryString["modalwin"] == "1")
                MasterPageFile = "~/ControlPanel/Dummy.master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // page validation

                // hide unrelated data for model win.
                if (!string.IsNullOrEmpty(Request.QueryString["modalwin"]) && Request.QueryString["modalwin"] == "1")
                    trBredCrum.Visible = false;
                // get form/query string values.
                int nEditSchoolId = Utils.fixNullInt(Request.QueryString["SchoolId"]);

                // load edit user details.
                if (nEditSchoolId > 0)
                {
                    var oSchool = new School();
                    oSchool.SchoolID = nEditSchoolId;
                    var oSchoolManager = new SchoolManager(oSchool);
                    oSchool = oSchoolManager.Load();
                    hdnEditSchoolId.Value = oSchool.SchoolID.ToString();
                    txtSchoolName.Text = oSchool.SchoolName;
                    txtPrinName.Text = oSchool.PrincipalName;
                    txtSocialOrg.Text = oSchool.SocialOrganizerName;
                    txtAddress.Text = oSchool.Address;
                    txtEmail.Text = oSchool.EmailAddress;
                    txtPhone.Text = oSchool.PhoneNumber;
                    txtNote.Text = oSchool.Notes;
                }
            }
        }
    }
}