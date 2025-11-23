using System;
using System.IO;
using System.Web.UI;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel
{
    public partial class DetailsFund : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // page validation

                // get form/query string values.
                int nEditFundId = Utils.fixNullInt(Request.QueryString["FundId"]);

                var oFund = new Fund();
                oFund.FundID = nEditFundId;
                var oFundManager = new FundManager(oFund);
                oFund = oFundManager.Load();

                // load edit user details.
                if (nEditFundId > 0)
                {
                    var oDonor = new User();
                    oDonor.UserID = oFund.DonorID;
                    oDonor.UserRoleID = 4;
                    var oUserManager1 = new UserManager(oDonor);
                    oDonor = oUserManager1.Load();

                    lblFName.Text = oDonor.FirstName;
                    lblLName.Text = oDonor.LastName;
                    if (SaveDCSession.UserAccessLevel == UserAccessLevels.SuperAdmin || SaveDCSession.UserAccessLevel == UserAccessLevels.Admin)
                    {
                        lblEmail.Text = oDonor.EmailAddress;
                        lblPhone.Text = oDonor.PhoneNumber;
                    }
                    lblDonor.Text = oDonor.UserName;

                    lblFundedOn.Text = oFund.FundDateShortString;
                    lblPostedBy.Text = oFund.FundPostedName;
                    txtAmount.Text = oFund.FundAmount.ToString();
                    txtNote.Text = oFund.Note;

                    string szViewHTML =
                        "window.open('{0}?modalwin=1', 'View', 'left=100,top=30,screenX=100,screenY=30, height=550,width=840,toolbar=no,directories=no,status=no,menubar=no,modal=yes,scrollbars=yes');";
                    string szServerPath = Server.MapPath(SaveDCConstants.CertificateUploadPath) + oFund.Attachment + ".jpg";
                    if (File.Exists(szServerPath))
                    {
                        ViewAttach.Attributes.Add("onclick",
                                                 string.Format(szViewHTML,
                                                               SaveDCConstants.CertificateUploadPath + oFund.Attachment +
                                                               ".jpg"));
                    }
                    else
                    {
                        ViewAttach.Parent.Controls.Remove(ViewAttach);
                    }
                }
            }
        }
    }
}