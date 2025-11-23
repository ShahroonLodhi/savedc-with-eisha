using System;
using System.Web.UI;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class PrintDetailsSchool : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // page validation

                // get form/query string values.
                int nEditSchoolId = Utils.fixNullInt(Request.QueryString["SchoolId"]);

                // load edit user details.
                if (nEditSchoolId > 0)
                {
                    var oSchool = new School();
                    oSchool.SchoolID = nEditSchoolId;
                    var oSchoolManager = new SchoolManager(oSchool);
                    oSchool = oSchoolManager.Load();
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